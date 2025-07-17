using Microsoft.AspNetCore.Mvc;
using ProcessEngine.Models;
using ProcessEngine.Services;

namespace ProcessEngine.Controllers
{
    /// <summary>
    /// API controller for managing process runs (runtime operations).
    /// </summary>
    [ApiController]
    [Route("api/process-runs")]
    public class ProcessRunsController : ControllerBase
    {
        private readonly ProcessManager _manager;

        /// <summary>
        /// Constructor with dependency injection of the manager.
        /// </summary>
        public ProcessRunsController(ProcessManager manager)
        {
            _manager = manager;
        }

        /// <summary>
        /// Starts a new process run for a given process blueprint.
        /// </summary>
        /// <param name="processBlueprintId">The process blueprint ID.</param>
        /// <returns>The created process run or an error.</returns>
        [HttpPost("/api/process-blueprints/{processBlueprintId}/runs")]
        public IActionResult StartProcessRun(string processBlueprintId)
        {
            var blueprint = _manager.GetBlueprint(processBlueprintId);
            if (blueprint == null)
                return NotFound($"Process blueprint '{processBlueprintId}' not found.");

            var initialStage = blueprint.Stages.FirstOrDefault(s => s.IsInitial);
            if (initialStage == null)
                return BadRequest("Process blueprint does not have an initial stage.");

            var run = new ProcessRun
            {
                Id = Guid.NewGuid().ToString(),
                ProcessBlueprintId = processBlueprintId,
                CurrentStageId = initialStage.Id,
                Log = new List<StepLog>()
            };
            _manager.AddRun(run);
            return CreatedAtAction(nameof(GetProcessRun), new { id = run.Id }, run);
        }

        /// <summary>
        /// Executes a step on a process run, moving it to the target stage if valid.
        /// </summary>
        /// <param name="id">The process run ID.</param>
        /// <param name="request">The step execution request (stepId).</param>
        /// <returns>The updated process run or an error.</returns>
        [HttpPost("{id}/steps")]
        public IActionResult ExecuteStep(string id, [FromBody] ExecuteStepRequest request)
        {
            var run = _manager.GetRun(id);
            if (run == null)
                return NotFound($"Process run '{id}' not found.");

            var blueprint = _manager.GetBlueprint(run.ProcessBlueprintId);
            if (blueprint == null)
                return NotFound($"Process blueprint '{run.ProcessBlueprintId}' not found.");

            var currentStage = blueprint.Stages.FirstOrDefault(s => s.Id == run.CurrentStageId);
            if (currentStage == null)
                return BadRequest("Current stage not found in process blueprint.");

            if (currentStage.IsFinal)
                return BadRequest("Cannot execute steps on a final stage.");

            var step = blueprint.Steps.FirstOrDefault(a => a.Id == request.StepId);
            if (step == null)
                return BadRequest($"Step '{request.StepId}' not found in process blueprint.");

            if (!step.Enabled)
                return BadRequest($"Step '{step.Name}' is not enabled.");

            if (!step.FromStages.Contains(currentStage.Id))
                return BadRequest($"Step '{step.Name}' cannot be executed from the current stage '{currentStage.Name}'.");

            var targetStage = blueprint.Stages.FirstOrDefault(s => s.Id == step.ToStage);
            if (targetStage == null)
                return BadRequest($"Target stage '{step.ToStage}' not found in process blueprint.");

            // Update run stage and log
            run.CurrentStageId = targetStage.Id;
            run.Log.Add(new StepLog
            {
                StepId = step.Id,
                Timestamp = DateTime.UtcNow
            });
            _manager.UpdateRun(run);
            return Ok(run);
        }

        /// <summary>
        /// Retrieves the current stage and log of a process run.
        /// </summary>
        /// <param name="id">The process run ID.</param>
        /// <returns>The process run or NotFound.</returns>
        [HttpGet("{id}")]
        public IActionResult GetProcessRun(string id)
        {
            var run = _manager.GetRun(id);
            if (run == null)
                return NotFound();
            return Ok(run);
        }

        /// <summary>
        /// Request model for executing a step on a process run.
        /// </summary>
        public class ExecuteStepRequest
        {
            /// <summary>
            /// The ID of the step to execute.
            /// </summary>
            public string StepId { get; set; }
        }
    }
} 
using Microsoft.AspNetCore.Mvc;
using ProcessEngine.Models;
using ProcessEngine.Services;

namespace ProcessEngine.Controllers
{
    /// <summary>
    /// API controller for managing process blueprints.
    /// </summary>
    [ApiController]
    [Route("api/process-blueprints")]
    public class ProcessBlueprintsController : ControllerBase
    {
        private readonly ProcessManager _manager;

        /// <summary>
        /// Constructor with dependency injection of the manager.
        /// </summary>
        public ProcessBlueprintsController(ProcessManager manager)
        {
            _manager = manager;
        }

        /// <summary>
        /// Registers a new process blueprint.
        /// </summary>
        /// <param name="blueprint">The process blueprint to register.</param>
        /// <returns>The created process blueprint or a validation error.</returns>
        [HttpPost]
        public IActionResult RegisterBlueprint([FromBody] ProcessBlueprint blueprint)
        {
            // Validation: must have exactly one initial stage
            if (blueprint.Stages == null || blueprint.Stages.Count == 0)
                return BadRequest("A process must have at least one stage.");

            var initialStages = blueprint.Stages.Where(s => s.IsInitial).ToList();
            if (initialStages.Count != 1)
                return BadRequest("A process must have exactly one initial stage.");

            // Check for duplicate stage IDs
            if (blueprint.Stages.Select(s => s.Id).Distinct().Count() != blueprint.Stages.Count)
                return BadRequest("Stage IDs must be unique.");

            // Check for duplicate step IDs
            if (blueprint.Steps != null && blueprint.Steps.Select(a => a.Id).Distinct().Count() != blueprint.Steps.Count)
                return BadRequest("Step IDs must be unique.");

            // Check that all referenced stages in steps exist
            if (blueprint.Steps != null)
            {
                var stageIds = blueprint.Stages.Select(s => s.Id).ToHashSet();
                foreach (var step in blueprint.Steps)
                {
                    if (!stageIds.Contains(step.ToStage) || step.FromStages.Any(f => !stageIds.Contains(f)))
                        return BadRequest($"Step '{step.Name}' references unknown stage(s).");
                }
            }

            // Register the process blueprint
            _manager.RegisterBlueprint(blueprint);
            return CreatedAtAction(nameof(GetBlueprint), new { id = blueprint.Id }, blueprint);
        }

        /// <summary>
        /// Retrieves a process blueprint by ID.
        /// </summary>
        /// <param name="id">The process blueprint ID.</param>
        /// <returns>The process blueprint or NotFound.</returns>
        [HttpGet("{id}")]
        public IActionResult GetBlueprint(string id)
        {
            var blueprint = _manager.GetBlueprint(id);
            if (blueprint == null)
                return NotFound();
            return Ok(blueprint);
        }
    }
} 
namespace ProcessEngine.Models
{
    /// <summary>
    /// Represents a single execution of a process, tracking its current stage and step log.
    /// </summary>
    /// <remarks>
    /// Each process run is created from a process blueprint and maintains its own state and history.
    /// </remarks>
    public class ProcessRun
    {
        /// <summary>
        /// Unique identifier for this process run instance.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The ID of the process blueprint this run is based on.
        /// </summary>
        public string ProcessBlueprintId { get; set; }

        /// <summary>
        /// The current stage ID of the process run.
        /// </summary>
        public string CurrentStageId { get; set; }

        /// <summary>
        /// Chronological log of all steps executed during this run.
        /// </summary>
        public List<StepLog> Log { get; set; } = new List<StepLog>();
    }
} 
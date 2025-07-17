namespace ProcessEngine.Models
{
    /// <summary>
    /// Defines a possible step (transition) between stages in a process blueprint.
    /// </summary>
    /// <remarks>
    /// Steps control how a process run can move from one stage to another.
    /// </remarks>
    public class StepTransition
    {
        /// <summary>
        /// Unique identifier for the step transition.
        /// </summary>
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// Human-readable name for the step (e.g., "Approve", "Reject").
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Indicates if this step is currently enabled for use in process runs.
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// List of stage IDs from which this step can be executed.
        /// </summary>
        public List<string> FromStages { get; set; } = new List<string>();

        /// <summary>
        /// The stage ID to which this step transitions.
        /// </summary>
        public string ToStage { get; set; } = string.Empty;
    }
} 
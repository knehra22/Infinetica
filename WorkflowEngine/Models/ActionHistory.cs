namespace ProcessEngine.Models
{
    /// <summary>
    /// Records the execution of a step during a process run, including timestamp and step ID.
    /// </summary>
    /// <remarks>
    /// Used for auditing and tracking the history of a process run.
    /// </remarks>
    public class StepLog
    {
        /// <summary>
        /// The unique identifier of the step that was executed.
        /// </summary>
        public string StepId { get; set; } = string.Empty;

        /// <summary>
        /// The UTC timestamp when the step was executed.
        /// </summary>
        public DateTime Timestamp { get; set; }
    }
} 
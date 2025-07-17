namespace ProcessEngine.Models
{
    /// <summary>
    /// Represents a possible stage in a process, such as "Draft", "Approved", or "Completed".
    /// </summary>
    /// <remarks>
    /// Stages define the possible states a process run can be in, as defined by a process blueprint.
    /// </remarks>
    public class Stage
    {
        /// <summary>
        /// Unique identifier for the stage.
        /// </summary>
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// Human-readable name for the stage.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Indicates if this is the initial stage of the process.
        /// </summary>
        public bool IsInitial { get; set; }

        /// <summary>
        /// Indicates if this is a final (terminal) stage.
        /// </summary>
        public bool IsFinal { get; set; }

        /// <summary>
        /// Indicates if this stage is currently enabled for use in process runs.
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Optional description for the stage, providing additional context or instructions.
        /// </summary>
        public string? Description { get; set; }
    }
} 
namespace ProcessEngine.Models
{
    /// <summary>
    /// Defines the structure and allowed transitions of a business process.
    /// A process blueprint specifies all possible stages and steps for a process type.
    /// </summary>
    /// <remarks>
    /// Blueprints are used to instantiate and validate process runs at runtime.
    /// </remarks>
    public class ProcessBlueprint
    {
        /// <summary>
        /// Unique identifier for the process blueprint. Used as a reference key.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Human-readable name for the process type (e.g., "Leave Request Workflow").
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// All possible stages that a process run can be in, as defined by this blueprint.
        /// </summary>
        public List<Stage> Stages { get; set; }

        /// <summary>
        /// All allowed step transitions between stages for this process type.
        /// </summary>
        public List<StepTransition> Steps { get; set; }
    }
} 
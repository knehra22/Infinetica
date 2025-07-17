using ProcessEngine.Models;

namespace ProcessEngine.Services
{
    /// <summary>
    /// Provides in-memory management for process blueprints and their runtime executions (process runs).
    /// This class acts as a simple, thread-safe data store for the application, supporting registration, retrieval, and update operations.
    /// </summary>
    /// <remarks>
    /// All data is stored in static collections and will be lost when the application restarts.
    /// This class is intended for demonstration and prototyping purposes only.
    /// </remarks>
    public class ProcessManager
    {
        // Static collections to store data in-memory.
        // These are shared across all requests and lost when the app restarts.

        /// <summary>
        /// Stores all process blueprints, keyed by their unique ID.
        /// </summary>
        private static readonly Dictionary<string, ProcessBlueprint> _processBlueprints = new();

        /// <summary>
        /// Stores all process runs, keyed by their unique ID.
        /// </summary>
        private static readonly Dictionary<string, ProcessRun> _processRuns = new();

        /// <summary>
        /// Registers a new process blueprint, or updates an existing one with the same ID.
        /// </summary>
        /// <param name="blueprint">The process blueprint to register. Must have a unique ID.</param>
        /// <remarks>
        /// If a blueprint with the same ID already exists, it will be overwritten.
        /// </remarks>
        public void RegisterBlueprint(ProcessBlueprint blueprint)
        {
            _processBlueprints[blueprint.Id] = blueprint;
        }

        /// <summary>
        /// Retrieves a process blueprint by its unique identifier.
        /// </summary>
        /// <param name="id">The unique ID of the process blueprint.</param>
        /// <returns>The matching <see cref="ProcessBlueprint"/>, or <c>null</c> if not found.</returns>
        public ProcessBlueprint? GetBlueprint(string id)
        {
            _processBlueprints.TryGetValue(id, out var blueprint);
            return blueprint;
        }

        /// <summary>
        /// Returns all registered process blueprints in the system.
        /// </summary>
        /// <returns>An enumerable collection of all <see cref="ProcessBlueprint"/> objects.</returns>
        public IEnumerable<ProcessBlueprint> GetAllBlueprints()
        {
            return _processBlueprints.Values;
        }

        /// <summary>
        /// Adds a new process run, or updates an existing one with the same ID.
        /// </summary>
        /// <param name="run">The process run to add or update.</param>
        /// <remarks>
        /// If a run with the same ID already exists, it will be overwritten.
        /// </remarks>
        public void AddRun(ProcessRun run)
        {
            _processRuns[run.Id] = run;
        }

        /// <summary>
        /// Retrieves a process run by its unique identifier.
        /// </summary>
        /// <param name="id">The unique ID of the process run.</param>
        /// <returns>The matching <see cref="ProcessRun"/>, or <c>null</c> if not found.</returns>
        public ProcessRun? GetRun(string id)
        {
            _processRuns.TryGetValue(id, out var run);
            return run;
        }

        /// <summary>
        /// Returns all process runs currently tracked by the system.
        /// </summary>
        /// <returns>An enumerable collection of all <see cref="ProcessRun"/> objects.</returns>
        public IEnumerable<ProcessRun> GetAllRuns()
        {
            return _processRuns.Values;
        }

        /// <summary>
        /// Updates an existing process run in the system.
        /// </summary>
        /// <param name="run">The process run to update. Must have a valid ID.</param>
        /// <remarks>
        /// If the run does not exist, it will be added as a new entry.
        /// </remarks>
        public void UpdateRun(ProcessRun run)
        {
            _processRuns[run.Id] = run;
        }
    }
} 
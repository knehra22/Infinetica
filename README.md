# ProcessEngine

A simple .NET 9 Web API for defining, managing, and executing business process blueprints and their runtime instances (process runs). This project is intended for demonstration, prototyping, and intern assessment purposes.

---

## Quick Start

### Prerequisites
- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0) (Preview or latest)
- Windows, Linux, or macOS

### Build & Run

1. **Navigate to the project directory:**
   ```sh
   cd Infonetica_Intern/WorkflowEngine
   ```
2. **Restore dependencies:**
   ```sh
   dotnet restore
   ```
3. **Build the project:**
   ```sh
   dotnet build
   ```
4. **Run the API:**
   ```sh
   dotnet run
   ```
5. **Access Swagger UI:**
   - Open your browser at [https://localhost:5001/swagger](https://localhost:5001/swagger) (or the port shown in your console)

### Example API Usage
- Use the Swagger UI to try endpoints for registering blueprints, starting process runs, and executing steps.
- You can also use the provided `WorkflowEngine.http` file for sample HTTP requests (update URLs as needed).

---

## Environment Notes
- Designed for .NET 9 (Preview or latest).
- No external database required; all data is stored in-memory and will be lost on restart.
- Runs on Kestrel web server by default.

---

## Assumptions & Shortcuts
- **In-memory storage:** All process blueprints and runs are stored in static collections for simplicity.
- **No authentication/authorization:** The API is open for demonstration purposes.
- **No persistent storage:** Data is lost when the app stops.
- **Minimal validation:** Only basic checks for unique IDs and valid transitions.
- **No concurrency handling:** Not thread-safe for production use.
- **Swagger enabled in development:** For easy API exploration.

---

## Known Limitations
- **Not production-ready:** Intended for learning, prototyping, and assessment only.
- **No user management or security.**
- **No database integration.**
- **No advanced error handling or logging.**
- **No support for distributed or multi-instance deployments.**
- **No unit or integration tests included.**

---

## Contact
For questions or feedback, please contact the project maintainer or your assessment supervisor.



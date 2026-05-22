using Cafeteria.Domain.Common;

namespace Cafeteria.Domain.WorkStations;

public sealed class CollaboratorWorkStationAssignment : Entity
{
    public Guid CollaboratorUserId { get; private set; }
    public Guid WorkStationId { get; private set; }
    public DateTime AssignedAtUtc { get; private set; }
    public bool IsActive { get; private set; }

    private CollaboratorWorkStationAssignment()
    {
    }

    public CollaboratorWorkStationAssignment(Guid collaboratorUserId, Guid workStationId, DateTime assignedAtUtc)
    {
        if (collaboratorUserId == Guid.Empty)
            throw new ArgumentException("Collaborator user id is required.", nameof(collaboratorUserId));

        if (workStationId == Guid.Empty)
            throw new ArgumentException("Work station id is required.", nameof(workStationId));

        CollaboratorUserId = collaboratorUserId;
        WorkStationId = workStationId;
        AssignedAtUtc = assignedAtUtc;
        IsActive = true;
    }

    public void Activate() => IsActive = true;

    public void Deactivate() => IsActive = false;
}
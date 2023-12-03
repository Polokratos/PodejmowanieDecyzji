namespace DecisionMakingServer.Enums;

public enum Status
{
    Ok,
    InvalidUsername,
    InvalidPassword,
    AlreadyExistsInDb,
    DatabaseAddError,
    InvalidSession
}
﻿namespace KingReserveBack.StaffManagement.Interfaces.Acl;

public interface IIamContextFacade
{
    Task<int> CreateUser(string username, string password);
    Task<int> FetchUserIdByUsername(string username);
    Task<string> FetchUsernameByUserId(int userId);
}
﻿namespace People.Application.Common;

public interface ICurrentUserService
{
    string? UserId { get; }
}
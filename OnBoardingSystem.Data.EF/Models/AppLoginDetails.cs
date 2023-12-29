﻿using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class AppLoginDetails
{
    public string RequestNo { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string? UserName { get; set; }

    public string? IsActive { get; set; }

    public string? IsPasswordChanged { get; set; }

    public DateTime? LastLoginTime { get; set; }

    public string? LastLoginIp { get; set; }

    public string? Password { get; set; }

    public string? PasswordHistory1 { get; set; }

    public string? PasswordHistory2 { get; set; }

    public string? PasswordHistory3 { get; set; }

    public string? AuthenticationType { get; set; }

    public string? SecurityQuestionId { get; set; }

    public string? SecurityAnswer { get; set; }

    public DateTime? LastFailedLoginAttemptTime { get; set; }

    public string? LastFailedLoginAttemptIp { get; set; }

    public int? FailedLoginAttemptCount { get; set; }

    public DateTime? LastSuccessfulLoginTime { get; set; }

    public string? LastSuccessfulLoginIp { get; set; }

    public DateTime? LastPasswordChangeTime { get; set; }

    public string? LastPasswordChangeIp { get; set; }

    public DateTime? LastPasswordResetTime { get; set; }

    public string? LastPasswordResetIp { get; set; }

    public string? UserToken { get; set; }

    public string? AccessToken { get; set; }

    public string? Designation { get; set; }

    public string? EmailId { get; set; }

    public string? MobileNo { get; set; }
}

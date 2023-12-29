﻿//-----------------------------------------------------------------------
// <copyright file="ZmstApplicationSummary.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class ApplicationSummary
    {
        public string? AgencyId { get; set; }

        public string? AppYear { get; set; }

        public string? AppType { get; set; }

        public string? AppId { get; set; }

        public string? AppTitle { get; set; }

        public string? AppUrl { get; set; }

        public string? Summary { get; set; }

        public string? Status { get; set; }

        public string? ApiLink { get; set; }

        public DateTime? UpdatedTime { get; set; }

        public string? UpdatedBy { get; set; }

        public string? IpAddress { get; set; }

        public int? Priority { get; set; }

        public byte[]? ScheduleDoc { get; set; }

        public int Id { get; set; }

        public int? TotalRound { get; set; }

        public string? AdminUrl { get; set; }

        public string? ContactDetail { get; set; }

    }
}

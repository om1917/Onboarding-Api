//-----------------------------------------------------------------------
// <copyright file="AdvanceSearch.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class AdvanceSearch
    {
        public DateTime? workorderTo { get; set; }
        public string? agencyName { get; set; }
        public string? Division { get; set; }
        public string? EmpStatus { get; set; }
    }
}
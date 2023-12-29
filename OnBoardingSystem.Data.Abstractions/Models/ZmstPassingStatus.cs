//-----------------------------------------------------------------------
// <copyright file="ZmstPassingStatus.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class ZmstPassingStatus
    {
		public string PassingStatusId{ get; set; }
			public string Description{ get; set; }
			public string? AlternateNames{ get; set; }
			
			}
		}
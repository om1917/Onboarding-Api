﻿using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class AppDocumentUploadedDetail
{
    public int DocumentId { get; set; }

    public string? Activityid { get; set; }

    /// <summary>
    /// Mater Table Id
    /// </summary>
    public string ModuleRefId { get; set; } = null!;

    public string DocType { get; set; } = null!;

    public string? DocId { get; set; }

    public string? DocSubject { get; set; }

    public string? DocContent { get; set; }

    public string? DocContentType { get; set; }

    public string? DocFileName { get; set; }

    public string? ObjectId { get; set; }

    public string? ObjectUrl { get; set; }

    public string? DocNatureId { get; set; }

    public string? IpAddress { get; set; }

    public DateTime? SubTime { get; set; }

    public string? CreatedBy { get; set; }

    public int? VersionNo { get; set; }
}

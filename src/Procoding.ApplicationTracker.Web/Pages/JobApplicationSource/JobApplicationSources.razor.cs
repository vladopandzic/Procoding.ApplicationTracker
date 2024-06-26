﻿
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Procoding.ApplicationTracker.Web.ViewModels.JobApplicationSources;

namespace Procoding.ApplicationTracker.Web.Pages.JobApplicationSource;

[Authorize(Roles = "Employee")]
public partial class JobApplicationSources
{
    [Inject]
    public JobApplicationSourceListViewModel ViewModel { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        await ViewModel.InitializeViewModel();
        await base.OnInitializedAsync();
    }
}

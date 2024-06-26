﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Procoding.ApplicationTracker.DTOs.Model;
using Procoding.ApplicationTracker.DTOs.Request.Candidates;
using Procoding.ApplicationTracker.Web.Auth;
using Procoding.ApplicationTracker.Web.ViewModels.Candidates;

namespace Procoding.ApplicationTracker.Web.Pages.Candidate;

[Authorize(Roles = "Employee")]
public partial class CandidateListPage
{
    [Inject]
    public CandidateListViewModel ViewModel { get; set; } = default!;

    private async Task<GridData<CandidateDTO>> LoadGridData(GridState<CandidateDTO> state)
    {

        ViewModel.Request = GridStateConverter.ConvertToRequest<EmployeeGetListRequestDTO, CandidateDTO>(state);

        await ViewModel.GetCandidates();

        GridData<CandidateDTO> data = new GridData<CandidateDTO>
        {
            Items = ViewModel.Candidates,
            TotalItems = ViewModel.TotalNumberOfCandidates
        };
        return data;
    }
}

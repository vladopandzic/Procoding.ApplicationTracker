using Microsoft.AspNetCore.Components;
using MudBlazor;
using Procoding.ApplicationTracker.DTOs.Model;
using Procoding.ApplicationTracker.Web.ViewModels.Candidates;

namespace Procoding.ApplicationTracker.Web.Pages.Candidate;

public partial class CandidateListPage
{
    [Inject]
    public CandidateListViewModel ViewModel { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        await ViewModel.InitializeViewModel();
        await base.OnInitializedAsync();
    }

    private async Task<GridData<CandidateDTO>> LoadGridData(GridState<CandidateDTO> state)
    {
        ViewModel.Request.PageSize = state.PageSize;
        ViewModel.Request.PageNumber = state.Page + 1;
        ViewModel.Request.Filters = state.FilterDefinitions.Select(x => new DTOs.Request.Base.FilterModelDto()
        {
            Key = x.Title,
            Value = x.Value as string,
            Operator = x.Operator
        }).ToList();
        
        await ViewModel.GetCandidates();



        GridData<CandidateDTO> data = new GridData<CandidateDTO>
        {
            Items = ViewModel.Candidates,
            TotalItems = ViewModel.TotalNumberOfCandidates
        };
        return data;
    }
}

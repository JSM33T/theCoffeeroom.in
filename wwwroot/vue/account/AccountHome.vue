<template>
    <div class="d-flex align-items-center mt-sm-n1 pb-4 mb-0 mb-lg-1 mb-xl-3">
        <i class="ai-user text-primary lead pe-1 me-2"></i>
        <h2 class="h4 mb-0">Basic info</h2><a class="btn btn-sm btn-secondary ripple ms-auto" href="/account/profile"><i class="ai-edit ms-n1 me-2"></i>Edit info</a>
    </div>
    <div class="d-md-flex align-items-center">
        <div class="d-sm-flex align-items-center">
            @{
            var avtr = HttpContext.Session.GetString("avatar") + ".png";
            }
            <div class="rounded-circle bg-size-cover bg-position-center flex-shrink-0" style="width: 80px; height: 80px; background-image: url(/assets/images/avatars/default/@avtr);"></div>
            <div class="pt-3 pt-sm-0 ps-sm-3">
                <h3 class="h5 mb-2">@Model.UserDetailsDisplay.FirstName @Model.UserDetailsDisplay.LastName<i class="ai-circle-check-filled fs-base text-success ms-2"></i></h3>
                <div class="text-muted fw-medium d-flex flex-wrap flex-sm-nowrap align-iteems-center">
                    <div class="d-flex align-items-center me-3"><i class="ai-mail me-1"></i>@Model.UserDetailsDisplay.EMail</div>
                </div>
            </div>
        </div>
    </div>
    <div class="row py-3 mb-2 mb-sm-3">
        <div class="col-md-6 mb-4 mb-md-0">
            <table class="table mb-0">
                <tr>
                    <td class="border-0 text-muted py-1 px-0">Username</td>
                    <td class="border-0 text-dark fw-medium py-1 ps-3">@Model.UserDetailsDisplay.UserName</td>
                </tr>
                <tr>
                    <td class="border-0 text-muted py-1 px-0">Phone</td>
                    <td class="border-0 text-dark fw-medium py-1 ps-3">@Model.UserDetailsDisplay.Phone</td>
                </tr>
                <tr>
                    <td class="border-0 text-muted py-1 px-0">EMail</td>
                    <td class="border-0 text-dark fw-medium py-1 ps-3">@Model.UserDetailsDisplay.EMail</td>
                </tr>
                <tr>
                    <td class="border-0 text-muted py-1 px-0">Gender</td>
                    @{
                    var gndrfetch = Model.UserDetailsDisplay.Gender;
                    if (gndrfetch == "m")
                    {
                    gndr = "Male";
                    }
                    else if (gndrfetch == "f")
                    {
                    gndr = "Female";
                    }
                    else
                    {
                    gndr = "Others";
                    }
                    }
                    <td class="border-0 text-dark fw-medium py-1 ps-3">@gndr</td>
                </tr>
                <tr>
                    <td class="border-0 text-muted py-1 px-0">Bio</td>
                    <td class="border-0 text-dark fw-medium py-1 ps-3">@Model.UserDetailsDisplay.Bio</td>
                </tr>
            </table>
        </div>
        <div class="col-md-6 d-md-flex justify-content-end">
            <div class="w-100 border rounded-3 p-4" style="max-width: 242px;">
                <img class="d-block mb-2" src="assets/img/account/gift-icon.svg" width="24" alt="Gift icon">
                <h4 class="h5 lh-base mb-0">(ID)</h4>
                <p class="fs-sm text-muted mb-0">birthday</p>
            </div>
        </div>
    </div>
</template>
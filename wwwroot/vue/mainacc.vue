
<template>
    <div class="fade-in">
        <div class="d-flex align-items-center mt-sm-n1 pb-4 mb-0 mb-lg-1 mb-xl-3">
            <i class="ai-edit text-primary lead pe-1 me-2"></i>
            <h2 class="h4 mb-0">Edit Info</h2><router-link class="btn btn-sm btn-secondary ripple ms-auto" to="/profile"><i class="ai-user ms-n1 me-2"></i>Dashboard</router-link>
        </div>
        <div id="profilepanel">
            <div class="d-md-flex align-items-center">
                <div class="d-sm-flex align-items-center">
                    <div id="avatar_placeholder" class="rounded-circle bg-size-cover bg-position-center flex-shrink-0" style="width: 80px; height: 80px; background-image: url(/assets/images/avatars/default/@avtrid);"></div>
                    <div class="pt-3 pt-sm-0 ps-sm-3">
                        <h3 class="h5 mb-2">
                            <span id="profilename">
                                {{firstname}} {{lastname}}
                            </span>
                        </h3>
                        <div class="text-muted fw-medium d-flex flex-wrap flex-sm-nowrap align-iteems-center">
                            <div class="d-flex align-items-center me-3">
                                <i class="ai-user me-1"></i>
                                {{role}}
                            </div>
                        </div>
                    </div>
                </div>
                <div class="w-100 pt-3 pt-md-0 ms-md-auto col-sm-12" style="max-width: 212px;">
                    <div class="d-flex justify-content-between fs-sm pb-1 mb-2">Select your avatar</div>
                    <select name="avatar" id="avatars" class="form-select" @@change="handleChange">
                        <option v-for="option in options" :value="option.image" :key="option.title">{{ option.title }}</option>
                    </select>
                </div>
            </div>
        </div>
        <div class="row g-3 g-sm-4 mt-0 mt-lg-2">
            <div class="col-sm-6">
                <label class="form-label" for="fn">First name</label>
                <input name="fname" type="text" id="firstname" class="form-control bindenter1" aria-autocomplete="none" value="@Model.FirstName" maxlength="20" />
            </div>
            <div class="col-sm-6">
                <label class="form-label" for="ln">Last name</label>
                <input name="lname" type="text" id="lastname" class="form-control bindenter1" aria-autocomplete="none" value="@Model.LastName" maxlength="20" />
            </div>
            <div class="col-sm-6">
                <label class="form-label" for="email">Email address</label>
                <input name="email" type="text" id="email" class="form-control bindenter1" placeholder="Enter your email" value="@Model.EMail" aria-autocomplete="none" maxlength="40" />
            </div>
            <div class="col-sm-6">
                <label class="form-label" for="phone">Phone <span class='text-muted'>(optional)</span></label>
                <input name="phone" type="text" id="phone" class="form-control bindenter1" value="@Model.Phone" placeholder="" maxlength="15" />
            </div>
            <div class="col-12">
                <label class="form-label" for="phone">Bio <span class='text-muted'>(optional)</span></label>
                <textarea name="bio" type="text" id="bio" class="form-control bindenter1" placeholder="enter a bio" maxlength="100">@Model.Bio</textarea>
            </div>

            <div class="col-sm-12 col-md-6  d-sm-flex align-items-center pt-sm-2 pt-md-3">
                <div class="form-label text-muted mb-2 mb-sm-0 me-sm-4">Gender:</div>
                <div class="form-check form-check-inline mb-0">
                    <input value="m" name="gender" type="radio" id="male" class="form-check-input" @(@Model.Gender= ="m" ? "checked" : "false" ) />
                    <label class="form-check-label" for="male">Male</label>
                </div>
                <div class="form-check form-check-inline mb-0">
                    <input value="f" name="gender" type="radio" id="female" class="form-check-input" @(@Model.Gender= ="f" ? "checked" : "false" ) />
                    <label class="form-check-label" for="female">Female</label>
                </div>
                <div class="form-check form-check-inline mb-0">
                    <input value="o" name="gender" type="radio" id="other" class="form-check-input" @(@Model.Gender= ="o" ? "checked" : "false" ) />
                    <label class="form-check-label" for="other">Other</label>
                </div>
            </div>
            <div class="col-12 d-flex justify-content-end pt-3">
                <button class="btn btn-primary ms-3 ripple" type="button" id="saveprof" onclick="save_prof()">Save Profile</button>
            </div>
        </div>
    </div>
    </template>
  
        data() {
        return {
        options: [],
        title: "Edit Info",
        username: "@Model.UserName",
        firstname: "@Model.FirstName",
        lastname: "@Model.LastName",
        email: "@Model.EMail",
        phone: "@Model.Phone",
        bio: "@Model.Bio",
        avatar: "@avtrid",
        gender: "@Model.Gender",
        role: "@userrole"
        };
        },
        async mounted() {
        window.scrollTo({
        top: 0,
        behavior: 'smooth',
        });
        this.fetchAvatars();

        },
        methods: {
        fetchAvatars() {
        axios.get("/api/getavatars")
        .then(response => {
        this.options = response.data;
        console.log(response.data);
        })
        .catch(error => {
        console.error('Error fetching data:', error);
        });
        },
        handleChange(event) {
        var dropdown = document.getElementById("avatars");
        var selectedOption = dropdown.options[dropdown.selectedIndex];
        alert(selectedOption.value);
        var selectedOptionDataId = selectedOption.getAttribute("image-link");
        //document.getElementById('avatar_placeholder').style.background-image = 'url(/assets/images/avatars/default/default.png)';
        }
        }
        };
        const HomeComponent = {
        template: `
        <div class="fade-in">
            <div class="d-flex align-items-center mt-sm-n1 pb-4 mb-0 mb-lg-1 mb-xl-3 ">
                <i class="ai-user text-primary lead pe-1 me-2"></i>
                <h2 class="h4 mb-0">Dashboard</h2><router-link class="btn btn-sm btn-secondary ripple ms-auto" to="/profile/edit"><i class="ai-edit ms-n1 me-2"></i>Edit info</router-link>
            </div>
            <div class="d-md-flex align-items-center">
                <div class="d-sm-flex align-items-center">
                    <div class="rounded-circle bg-size-cover bg-position-center flex-shrink-0" :style="{ width: '80px', height: '80px', backgroundImage:'url(/assets/images/avatars/default/' + avatar + ')' }"></div>
                    <div class="pt-3 pt-sm-0 ps-sm-3">
                        <h3 class="h5 mb-2">{{firstname}} {{lastname}}<i class="ai-circle-check-filled fs-base text-success ms-2"></i></h3>
                        <div class="text-muted fw-medium d-flex flex-wrap flex-sm-nowrap align-iteems-center">
                            <div class="d-flex align-items-center me-3"><i class="ai-mail me-1"></i>{{email}}</div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row py-3 mb-2 mb-sm-3">
                <div class="col-md-6 mb-4 mb-md-0">
                    <table class="table mb-0">
                        <tr>
                            <td class="border-0 text-muted py-1 px-0">Username</td>
                            <td class="border-0 text-dark fw-medium py-1 ps-3">{{username}}</td>
                        </tr>
                        <tr>
                            <td class="border-0 text-muted py-1 px-0">Phone</td>
                            <td class="border-0 text-dark fw-medium py-1 ps-3">{{phone}}</td>
                        </tr>
                        <tr>
                            <td class="border-0 text-muted py-1 px-0">EMail</td>
                            <td class="border-0 text-dark fw-medium py-1 ps-3">{{email}}</td>
                        </tr>
                        <tr>
                            <td class="border-0 text-muted py-1 px-0">Gender</td>

                            <td class="border-0 text-dark fw-medium py-1 ps-3">{{gender}}</td>
                        </tr>
                        <tr>
                            <td class="border-0 text-muted py-1 px-0">Bio</td>
                            <td class="border-0 text-dark fw-medium py-1 ps-3">{{bio}}</td>
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
        </div>
        `,
        data() {
        return {
        title: "Dashboard",
        username: "@Model.UserName",
        firstname: "@Model.FirstName",
        lastname: "@Model.LastName",
        email: "@Model.EMail",
        phone: "@Model.Phone",
        bio: "@Model.Bio",
        avatar: "@avtrid",
        gender: "@Model.Gender",
        role: "@userrole"

        };
        },
        async mounted() {
        //   document.getElementById("titleBlog").innerHTML = "Our Blogs";
        window.scrollTo({
        top: 0,
        behavior: 'smooth', // Use 'smooth' for smooth scrolling effect, 'auto' for instant scrolling
        });

        },
        };

        const routes = [
        { path: '/profile', component: HomeComponent },
        { path: '/profile/edit', component: ProfileComponent, props: true }
        ];

        const router = VueRouter.createRouter({
        history: VueRouter.createWebHistory(),
        routes
        });

        const app = Vue.createApp({
        data() {
        return {
        isLoading: true,
        viewTitle: "Profile",
        };
        },
        });
        app.use(router);
        app.mount('#app');

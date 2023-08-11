const Security =
{
    template: '<h1>under construction</h1>',
    data() {
        return {
            username: "Jasmeet Singh",
        };

    },
    async mounted() {
        window.scrollTo({
            top: 0,
            behavior: 'smooth',
        });
        this.onInit();
    },
    methods: {
        onInit() {
            toaster("success", "security tab reached");
        }

    }
}

const EditProfile = {
    template: `
            <div class="fade-in">
                              <div class="d-flex align-items-center mt-sm-n1 pb-4 mb-0 mb-lg-1 mb-xl-3">
                                        <i class="ai-edit text-primary lead pe-1 me-2"></i>
                                               <h2 class="h4 mb-0">Edit Info</h2><div class="ms-auto"><router-link class="btn btn-sm btn-secondary ripple" to="/profile"><i class="ai-user ms-n1 me-2"></i>Dashboard</router-link><router-link class="btn btn-sm btn-secondary ripple mx-2" to="/profile/security"><i class="ai-user ms-n1 me-2"></i>Security</router-link></div>
                                    </div>
                                    <div id="profilepanel">
                                        <div class="d-md-flex align-items-center">
                                            <div class="d-sm-flex align-items-center">
                                                <div id="avatar_placeholder" class="rounded-circle bg-size-cover bg-position-center flex-shrink-0" :style='"width:80px; height: 80px; background-image: url(/assets/images/avatars/default/${glAvatar});"'></div>
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
                                                <select name="avatar" id="avatars" class="form-select" @change="handleChange">
                                                         <option v-for="option in options" :data-id="option.id" :value="option.image" :key="option.title">{{ option.title }}</option>
                                                </select>
                                            </div>
                                        </div>
                                    </div>
                                            <div class="row g-3 g-sm-4 mt-0 mt-lg-2">
                                                <div class="col-sm-6">
                                                    <label class="form-label" for="fn">First name</label>
                                                    <input type="text" v-model="firstname" class="form-control " aria-autocomplete="none" value="" maxlength="20" />
                                                </div>
                                                <div class="col-sm-6">
                                                    <label class="form-label" for="ln">Last name</label>
                                                            <input type="text" v-model="lastname" class="form-control " aria-autocomplete="none" value="" maxlength="20" />
                                                </div>
                                                <div class="col-sm-6">
                                                    <label class="form-label" for="email">Email address</label>
                                                            <input type="text" v-model="email" class="form-control" placeholder="Enter your email" value="" aria-autocomplete="none" maxlength="40" />
                                                </div>
                                                <div class="col-sm-6">
                                                    <label class="form-label" for="phone">Phone <span class='text-muted'>(optional)</span></label>
                                                    <input v-model="phone" type="text" id="phone" class="form-control" value="" placeholder="" maxlength="15" />
                                                </div>
                                                <div class="col-12">
                                                    <label class="form-label" for="phone">Bio <span class='text-muted'>(optional)</span></label>
                                                    <textarea v-model="bio" type="text" id="bio" class="form-control" placeholder="enter a bio" maxlength="100"></textarea>
                                                </div>

                                                <div class="col-sm-12 col-md-6  d-sm-flex align-items-center pt-sm-2 pt-md-3">
                                                    <div class="form-label text-muted mb-2 mb-sm-0 me-sm-4">Gender:</div>
                                                    <div class="form-check form-check-inline mb-0">
                                                        <input value="m" v-model="gender" type="radio" id="male" class="form-check-input"/>
                                                        <label class="form-check-label" for="male">Male</label>
                                                    </div>
                                                    <div class="form-check form-check-inline mb-0">
                                                        <input value="f" v-model="gender" type="radio" id="female" class="form-check-input"/>
                                                        <label class="form-check-label" for="female">Female</label>
                                                    </div>
                                                    <div class="form-check form-check-inline mb-0">
                                                        <input value="o" v-model="gender" type="radio" id="other" class="form-check-input"/>
                                                        <label class="form-check-label" for="other">Other</label>
                                                    </div>
                                                </div>
                                                <div class="col-12 d-flex justify-content-end pt-3">
                                                    <button class="btn btn-primary ms-3 ripple" type="button" id="saveprof" @click="saveDetails">Save Profile</button>
                                                </div>
                                            </div>
                                            </div>
                                        `,
    data() {
        return {
            options: [],
            title: "Edit Info",
            username: glUsername,
            firstname: glFirstName,
            lastname: glLastName,
            email: glEmail,
            phone: glPhone,
            bio: glBio,
            avatar: glAvatar,
            gender: glGender,
            role: glRole,
            someThing: "2"
        };
    },
    async mounted() {
        window.scrollTo({
            top: 0,
            behavior: 'smooth',
        });
        this.fetchAvatars();
        this.selectValue();


    },
    methods: {
        selectValue() {
            this.$nextTick(() => {
                document.getElementById("avatars").value = glAvtImage;
            });

        },
        fetchAvatars() {
            axios.get("/api/getavatars")
                .then(response => {
                    this.options = response.data;
                    console.log(response.data);
                    this.selectValue();
                })
                .catch(error => {
                    console.error('Error fetching data:', error);
                });
        },
        saveDetails() {
            let dropdown = document.getElementById("avatars");
            let selectedOption = dropdown.options[dropdown.selectedIndex];
            let avtId = selectedOption.getAttribute("data-id");
            let avtVal = selectedOption.getAttribute("value");

            const data = {
                firstname: this.firstname,
                lastname: this.lastname,
                bio: this.bio,
                phone: this.phone,
                gender: this.gender,
                avatarId: avtId,
            };
            axios.post("/api/profile/update", data)
                .then(response => {
                    console.log(response.data);
                    document.getElementById('layout_pfp').src = '/assets/images/avatars/default/' + avtVal + '.png';
                    toaster("success", response.data);
                })
                .catch(error => {
                    console.error('Error fetching data:', error);
                    toaster("Error", "Something went wrong");
                });
        },
        handleChange(event) {
            var dropdown = document.getElementById("avatars");
            var selectedOption = dropdown.options[dropdown.selectedIndex];
            var selectedOptionDataId = selectedOption.getAttribute("data-id");
            var selectedOptionValue = selectedOption.getAttribute("value");
            document.getElementById('avatar_placeholder').style.backgroundImage = 'url(/assets/images/avatars/default/' + selectedOptionValue + '.png)';



        },
    },

};


const Dashboard = {
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
            username: glUsername,
            firstname: glFirstName,
            lastname: glLastName,
            email: glEmail,
            phone: glPhone,
            bio: glBio,
            avatar: glAvatar,
            gender: glGender,
            role: glRole,
        };
    },
    async mounted() {
        window.scrollTo({
            top: 0,
            behavior: 'smooth',
        });

    },
};





const routes = [
    { path: '/profile', component: Dashboard },
    { path: '/profile/edit', component: EditProfile },
    { path: '/profile/security', component: Security }
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
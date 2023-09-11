const Security = {
    template: `<div class="fade-in">
                              <div class="d-flex align-items-center mt-sm-n1 pb-4 mb-0 mb-lg-1 mb-xl-3">
                                       <div class="ms-auto"><router-link class=" btn btn-sm btn-secondary ripple" to="/profile"><i class="ai-user ms-n1 me-2"></i>Dashboard</router-link><router-link class="btn btn-sm btn-secondary ripple mx-2" to="/profile/edit"><i class="ai-user ms-n1 me-2"></i>Edit Profile</router-link></div>
                                    </div>
                                            <div class="row g-3 g-sm-4 mt-0 mt-lg-2">
                                            <div class="col-md-6">
                                                   <div class="password-toggle">
                                                    <input class="form-control form-control-lg ps-5" v-model="newPassword" type="password" @keyup.enter="saveCredentials" placeholder="Password" autocomplete="off" required="">
                                                    <label class="password-toggle-btn" aria-label="Show/hide password">
                                                      <input class="password-toggle-check" type="checkbox"><span class="password-toggle-indicator"></span>
                                                    </label>
                                                  </div>
                                                  </div>
                                        <div class="col-md-6">
                                                <div class="password-toggle">
                                                    <input class="form-control form-control-lg ps-5" v-model="confirmPassword" @keyup.enter="saveCredentials" type="password" placeholder="Password" autocomplete="off" required="">
                                                    <label class="password-toggle-btn" aria-label="Show/hide password">
                                                      <input class="password-toggle-check" type="checkbox"><span class="password-toggle-indicator"></span>
                                                    </label>
                                                  </div>
                                                  </div>

                                                <div class="col-12 d-flex justify-content-end pt-3">
                                                    <button class="btn btn-primary ms-3 ripple" type="button" id="saveprof" @click="saveCredentials">Save Profile</button>
                                                </div>
                                            </div>
                </div>`,
    data() {
        return {
            username: '',
            passWord: '',
        };

    },
    async mounted() {
        window.scrollTo({
            top: 0,
            behavior: 'smooth',
        });
    },
    methods: {
        saveCredentials() {
            if (this.newPassword === "" || this.newPassword == null)
            {
                toaster('error', 'please enter the password');
            }
            else if (this.newPassword.length < 6)
            {
                toaster('error', 'Password cannot be less than 6 characters');
            }
            else if (this.newPassword != this.confirmPassword)
            {
                toaster('error', 'Passwords don\'t match');
            }
            else {
                const data = {
                    password: this.newPassword,
                    confirmPassword: this.confirmPassword
                };
                const token = document.querySelector('input[name="__RequestVerificationToken"]').value;
                axios.defaults.headers.common['RequestVerificationToken'] = token;
                axios.post("/api/profile/password/update", data)
                    .then(response => {
                        toaster("success", response.data);
                    })
                    .catch(error => {
                        toaster("Error", error.response.data);
                    });
            }
          
            //toaster("success", "button clickworking");
        }

    }
}

const EditProfile = {
    template: `
            <div class="fade-in">
                              <div class="d-flex align-items-center mt-sm-n1 pb-4 mb-0 mb-lg-1 mb-xl-3">
                                      <div class="ms-auto"><router-link class="btn btn-sm btn-secondary ripple" to="/profile"><i class="ai-user ms-n1 me-2"></i>Dashboard</router-link><router-link class="btn btn-sm btn-secondary ripple mx-2" to="/profile/security"><i class="ai-user ms-n1 me-2"></i>Security</router-link></div>
                              </div>
                                    <div id="profilepanel">
                                        <div class="d-md-flex align-items-center">
                                            <div class="d-sm-flex align-items-center">
                                                <div class="rounded-circle bg-size-cover bg-position-center flex-shrink-0" :style="{ width: '80px', height: '80px', backgroundImage:'url(/assets/images/avatars/default/'+ avatar +')' }"></div>
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
                                                <select v-model="avatars" id="avatars" class="form-select" @change="handleChange">
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
                                                    <label class="form-label" for="username">userName <span class='text-muted'></span></label>
                                                    <input v-model="username" type="text" id="username" class="form-control" value="" placeholder="" maxlength="15" />
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
            username: '',
            firstname: '',
            lastname: '',
            email: '',
            phone: '',
            avatars: '',
            avatar: 'default.png',
            bio: '',
            gender: '',
            role: ''
        };
    },
    async mounted() {
        window.scrollTo({
            top: 0,
            behavior: 'smooth',
        });
       
        this.fetchAvatars();
        this.getDetails();
    },
    watch: {
        avatar(){},
        avatars(){ }
    },
    methods: {
        async getDetails() {
            await axios.get("/api/profile/getdetails")
                .then(response => {
                    console.log(response.data);
                    this.username = response.data.userName;
                    this.firstname = response.data.firstName;
                    this.lastname = response.data.lastName;
                    this.phone = response.data.phone;
                    this.bio = response.data.bio;
                    this.avatars = response.data.avatarImg;
                    this.avatar = response.data.avatarImg + ".png";
                    this.gender = response.data.gender;
                    this.role = response.data.role;

                })
                .catch(error => {
                    console.log(error);
                    //toaster("error", "something went wrong");
                    toaster("error", error.response.data);
                });
        },
        async fetchAvatars() {
            await axios.get("/api/getavatars")
                .then(response => {
                    this.options = response.data;
                })
                .catch(error => {
                    toaster("error", "something went wrong");
                });
        },
        async saveDetails() {

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
                username: this.username,
            };
            const token = document.querySelector('input[name="__RequestVerificationToken"]').value;
            axios.defaults.headers.common['RequestVerificationToken'] = token;
            axios.post("/api/profile/update", data)
                .then(response => {
                    document.getElementById('layout_pfp').src = '/assets/images/avatars/default/' + this.avatars + '.png';
                    toaster("success", response.data);
                    document.getElementById('title_master').innerHTML = this.firstname;
                })
                .catch(error => {
                    toaster("Error", error.response.data);
                    this.getDetails();
                });
        },
        async handleChange(event) {
            var dropdown = document.getElementById("avatars");
            var selectedOption = dropdown.options[dropdown.selectedIndex];
            var selectedOptionDataId = selectedOption.getAttribute("data-id");
            var selectedOptionValue = selectedOption.getAttribute("value");
            this.avatar = selectedOptionValue + ".png";
        }
    },
   
    

};


const Dashboard = {
    template: `
            <div class="fade-in">
                    <div class="d-flex align-items-center mt-sm-n1 pb-4 mb-0 mb-lg-1 mb-xl-3 ">
            <div class="ms-auto"><router-link class=" btn btn-sm btn-secondary ripple" to="/profile/edit"><i class="ai-user ms-n1 me-2"></i>Edit</router-link><router-link class="btn btn-sm btn-secondary ripple mx-2" to="/profile/security"><i class="ai-user ms-n1 me-2"></i>Security</router-link></div>
        </div>
        <div class="d-md-flex align-items-center">
            <div class="d-sm-flex align-items-center">
                <div class="rounded-circle bg-size-cover bg-position-center flex-shrink-0" :style="{ width: '80px', height: '80px', backgroundImage:'url(/assets/images/avatars/default/'+ avatar +')' }"></div>
                <div class="pt-3 pt-sm-0 ps-sm-3">
                    <h3 class="h5 mb-2">{{firstname}} {{lastname}} 
                    <span v-for="badge in badges" :key="badge.id">
                        <img :src="'/assets/svg/badges/' + badge.icon + '.svg'" style="width: 22px;" class="mx-1"  data-bs-toggle="tooltip" data-bs-placement="top" :title="badge.title"/>
                    </span>
                    </h3> 
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
                    <img class="d-block mb-2" src="/assets/img/account/gift-icon.svg" width="24" alt="Gift icon">
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
            username: '',
            firstname: '',
            lastname: '',
            email: '',
            phone: '',
            avatar: 'default.png',
            bio: '',
            gender: '',
            badges: [],
            role: ''
        };
    },
    async mounted() {
        window.scrollTo({
            top: 0,
            behavior: 'smooth',
        });
        this.getDetails();
        this.loadBadges();
    },
    methods: {
        async getDetails() {
            await axios.get("/api/profile/getdetails")
                .then(response => {
                    this.username = response.data.userName;
                    this.firstname = response.data.firstName;
                    this.lastname = response.data.lastName;
                    this.phone = response.data.phone;
                    this.bio = response.data.bio;
                    this.avatar = response.data.avatarImg + ".png";
                    this.gender = response.data.gender;
                    this.email = response.data.eMail;

                })
                .catch(error => {
                    console.log(error);
                    toaster("error", "something went wrong");
                });
        },
        async loadBadges() {
            await axios.get("/api/profile/userbadges")
                .then(response => {
                    this.badges = response.data;
                    console.log(response.data);
                })
                .catch(error => {
                    console.log(error);
                    toaster("error", "something went wrong");
                });
        }
    }
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
const routerViewElement = document.querySelector('router-view');

const LoginComponent = {
    template: `

    <a class="text-nav btn btn-icon bg-light border rounded-circle position-absolute top-0 end-0 p-0 mt-3 me-3 mt-sm-4 me-sm-4" href="/" data-bs-toggle="tooltip" data-bs-placement="left" title="Back to home"><i class="ai-home"></i></a>
    
    <div class="d-flex flex-column align-items-center w-lg-50 h-100 px-3 px-lg-5 pt-5">
        <div class="w-100 mt-auto" style="max-width: 526px;">
            <h1>Log In</h1>
            <p class="pb-3 mb-3 mb-lg-4">Don't have an account yet?&nbsp;&nbsp;<router-link to='/account/signup'>Register here!</router-link></p>
            <form class="needs-validation" id="loginForm" novalidate>
                <div class="pb-3 mb-3">
                    <div class="position-relative">
                        <i class="ai-mail fs-lg position-absolute top-50 start-0 translate-middle-y ms-3"></i>
                        <input class="form-control form-control-lg ps-5" v-model="username"  type="text" placeholder="Username or email" required>
                    </div>
                </div>
                <div class="mb-4">
                    <div class="position-relative">
                        <i class="ai-lock-closed fs-lg position-absolute top-50 start-0 translate-middle-y ms-3"></i>
                        <div class="password-toggle">
                            <input class="form-control form-control-lg ps-5" v-model="password" type="password" placeholder="Password" required>
                            <label class="password-toggle-btn" aria-label="Show/hide password">
                                <input class="password-toggle-check" type="checkbox"><span class="password-toggle-indicator"></span>
                            </label>
                        </div>
                    </div>
                </div>
                <div class="d-flex flex-wrap align-items-center justify-content-between pb-4">
                    <form-check class="my-1">
                        <input class="form-check-input" type="checkbox" id="keep-signedin" disabled>
                        <label class="form-check-label ms-1" for="keep-signedin">Keep me signed in (id)</label>
                    </form-check><a class="fs-sm fw-semibold text-decoration-none my-1" href="/account/password-recovery">Forgot password?</a>
                </div>
                <button class="btn btn-lg btn-primary w-100 mb-4" type="button" @click="submitLogin">Log In</button>
                <h2 class="h6 text-center pt-3 pt-lg-4 mb-4">Or sign in with your social account(id)</h2>
                <div class="row row-cols-1 row-cols-sm-2 gy-3">
                    <div class="col"><a class="btn btn-icon btn-outline-secondary btn-google btn-lg w-100 disabled" href="#"><i class="ai-google fs-xl me-2"></i>Google</a></div>
                    <div class="col"><a class="btn btn-icon btn-outline-secondary btn-facebook btn-lg w-100 disabled" href="#"><i class="ai-facebook fs-xl me-2"></i>Facebook</a></div>
                </div>
            </form>
        </div>
        <!-- Copyright-->
        <p class="w-100 fs-sm pt-5 mt-auto mb-5" style="max-width: 526px;"><span class="text-muted">thecoffeeroom.in</span></p>



                        

                `,
    data() {
        return {
            blogs: [],
        };
    },
    async mounted() {
        window.scrollTo({
            top: 0,
            behavior: 'smooth',
        });
    },
    methods: {
        submitLogin() {
            console.log("something");
                const details = {
                    UserName: this.username,
                    PassWord: this.password,
                };
                axios.post('/api/account/login', details)
                    .then((response) => {
                        window.location.href = localStorage.getItem("curr_link");
                        console.log("done");
                    })
                    .catch((error) => {
                        console.error('Error while sending POST request:', error);
                        console.log('done with error');
                    });
              
      
        }
    }
};

const SignupComponent = {
    template: `
        <a class="text-nav btn btn-icon bg-light border rounded-circle position-absolute top-0 end-0 p-0 mt-3 me-3 mt-sm-4 me-sm-4" href="/" data-bs-toggle="tooltip" data-bs-placement="left" title="Back to home"><i class="ai-home"></i></a>
    
    <div class="d-flex flex-column align-items-center w-lg-50 h-100 px-3 px-lg-5 pt-5">
        <div class="w-100 mt-auto" style="max-width: 526px;">
            <h1>Sign Up</h1>
            <p class="pb-3 mb-3 mb-lg-4">Don't have an account yet?&nbsp;&nbsp;<router-link to='/account/signup'>Register here!</router-link></p>
          <form id="signUpForm">
                <div class="row row-cols-1 row-cols-sm-2">
                    <div class="col mb-4">
                        <input class="form-control form-control-lg" name="firstname" v-model="firstname" type="text" placeholder="First Name" required maxlength="20">
                    </div>
                    <div class="col mb-4">
                        <input class="form-control form-control-lg" name="lastname" v-model="lastname" type="text" placeholder="Last Name" required maxlength="20">
                    </div>
                    <div class="col mb-4">
                        <input class="form-control form-control-lg" type="text" name="username" v-model="username" placeholder="Preferred UserName" required maxlength="20">
                    </div>
                </div>
                <div class="pb-3 mb-3">
                    <div class="position-relative">
                        <i class="ai-mail fs-lg position-absolute top-50 start-0 translate-middle-y ms-3"></i>
                        <input name="email" type="email" v-model="email" class="form-control form-control-lg ps-5 bindenter" placeholder="Email" maxlength="50" />
                    </div>
                </div>


                <div class="row row-cols-1 row-cols-sm-2 pb-2 mb-2">
                    <div class="position-relative">

                        <div class="password-toggle">
                            <i class="ai-lock-closed fs-lg position-absolute top-50 start-0 translate-middle-y ms-3"></i>
                            <input name="password" type="password" v-model="password" class="bindentersignup form-control form-control-lg ps-5 bindenter" placeholder="Password" maxlength="20" />
                            <label class="password-toggle-btn" aria-label="Show/hide password">
                                <input class="password-toggle-check" type="checkbox"><span class="password-toggle-indicator"></span>
                            </label>
                        </div>
                    </div>
                    <div class="position-relative">
                        <div class="password-toggle">
                            <i class="ai-lock-closed fs-lg position-absolute top-50 start-0 translate-middle-y ms-3"></i>
                            <input name="password" type="password" id="passwordconfirm" class="bindentersignup form-control form-control-lg ps-5 bindenter" placeholder="Confirm Pass" maxlength="20" />
                            <label class="password-toggle-btn" aria-label="Show/hide password">
                                <input class="password-toggle-check" type="checkbox"><span class="password-toggle-indicator"></span>
                            </label>
                        </div>
                    </div>
                </div>
                <button type="submit" id="signupbutton" class="btn btn-lg btn-primary w-100 mb-4">Sign Up</button>
              
            </form>
        </div>
        <!-- Copyright-->
        <p class="w-100 fs-sm pt-5 mt-auto mb-5" style="max-width: 526px;"><span class="text-muted">thecoffeeroom.in</span></p>
        
       

                `,
    data() {
        return {
            blogs: [],
        };
    },
    async mounted() {
        window.scrollTo({
            top: 0,
            behavior: 'smooth',
        });
        try {
            const response = axios.get('/api/blogs/0/na/na');
            const data = response.data;
            this.blogs = data;
            console.log(data);
        } catch (error) {
            console.error('Error fetching data from API:', error);
        } finally { }
    },
    methods: {
        submitSignUp() {
            console.log("something");
            const details = {
                UserName: this.username,
                PassWord: this.password,
            };
            axios.post('/api/account/login', details)
                .then((response) => {
                    window.location.href = localStorage.getItem("curr_link");
                    console.log("done");
                })
                .catch((error) => {
                    console.error('Error while sending POST request:', error);
                    console.log('done with error');
                });


        }
    }
};


const routes = [{
    path: '/account/',
    component: LoginComponent
}, {
    path: '/account/signup',
    component: SignupComponent
}];
const router = VueRouter.createRouter({
    history: VueRouter.createWebHistory(),
    routes
});
const app = Vue.createApp({
    data() {
        return {
            isLoading: true,
        };
    },
});
app.use(router);
app.mount('#app');
const routerViewElement = document.querySelector('router-view');

const BaseComponent = {
    template: `
          <a class="text-nav btn btn-icon bg-light border rounded-circle position-absolute top-0 end-0 p-0 mt-3 me-3 mt-sm-4 me-sm-4" href="/" data-bs-toggle="tooltip" data-bs-placement="left" title="Back to home"><i class="ai-home"></i></a>
            <div class="d-flex flex-column align-items-center w-lg-50 h-100 px-3 px-lg-5 pt-5 fade-in-smooth-pop bg-secondary">
            <div class="w-100 mt-auto" style="max-width: 526px;">
                <h1 class="pb-2 text-center">Account</h1>
                    <svg class="d-block pb-2 mb-2 mx-auto text-primary" width="511" height="40" viewBox="0 0 511 40" fill="currentColor" xmlns="http://www.w3.org/2000/svg">
                <path d="M385.252 0.160149C313.41 0.614872 292.869 0.910678 253.008 2.06539C211.7 3.26203 182.137 4.46154 135.231 6.84429C124.358 7.39665 112.714 7.92087 99.0649 8.47247C48.9242 10.4987 39.1671 11.0386 22.9579 12.6833C14.9267 13.4984 7.98117 14.0624 4.08839 14.2162C0.627411 14.3527 0 14.4386 0 14.7762C0 15.0745 5.53537 15.3358 8.56298 15.1804C9.64797 15.1248 12.5875 14.9887 15.0956 14.8782C17.6037 14.7676 23.123 14.4706 27.3608 14.2183C37.3399 13.6238 42.1312 13.4363 59.2817 12.9693C88.1133 12.1844 109.893 11.43 136.647 10.2898C146.506 9.86957 159.597 9.31166 165.737 9.04993C212.308 7.06466 269.195 5.29439 303.956 4.74892C346.139 4.08665 477.094 3.50116 474.882 3.98441C474.006 4.17607 459.021 5.6015 450.037 6.34782C441.786 7.03345 428 8.02235 411.041 9.14508C402.997 9.67773 391.959 10.4149 386.51 10.7832C366.042 12.1673 359.3 12.5966 347.67 13.2569C294.096 16.2987 258.708 18.9493 209.451 23.6091C180.174 26.3788 156.177 29.5584 129.396 34.2165C114.171 36.8648 112.687 37.3352 114.186 39.0402C115.161 40.1495 122.843 40.2933 138.338 39.492C166.655 38.0279 193.265 36.8923 219.043 36.048C235.213 35.5184 237.354 35.4296 253.795 34.6075C259.935 34.3005 270.549 33.8517 277.382 33.6105L289.804 33.1719L273.293 32.999C248.274 32.7369 221.435 32.7528 212.596 33.035C183.334 33.9693 167.417 34.6884 141.419 36.2506C135.222 36.623 129.994 36.8956 129.801 36.8566C127.94 36.4786 169.612 30.768 189.492 28.6769C234.369 23.956 280.582 20.4337 351.602 16.3207C358.088 15.9451 371.108 15.1233 380.535 14.4947C389.962 13.866 404.821 12.8761 413.556 12.2946C447.177 10.057 457.194 9.22358 489.506 5.97543C510.201 3.895 510.311 3.8772 510.875 2.50901C511.496 1.00469 509.838 0.322131 505.088 0.127031C500.576 -0.0584957 416.098 -0.0351424 385.252 0.160149ZM291.144 33.02C291.536 33.0658 292.102 33.0641 292.402 33.0162C292.703 32.9683 292.383 32.9308 291.691 32.9329C290.999 32.935 290.753 32.9743 291.144 33.02Z"></path>
              </svg>
                    <div class="row row-cols-1 row-cols-sm-2 row-cols-lg-2 g-4 pb-xl-2 pb-xxl-3">
                <!-- Item-->
                <div class="col">
                  <div class="card h-100 border-0 rounded-5">
                    <div class="card-body pb-3">
                      <svg class="d-block mt-1 mt-sm-0 mb-4" width="40" height="40" viewBox="0 0 40 40" xmlns="http://www.w3.org/2000/svg">
                        <g class="text-info">
                          <path d="M26.307 23.1116C26.307 28.3136 22.09 32.5307 16.888 32.5307C11.6859 32.5307 7.46891 28.3136 7.46891 23.1116C7.46891 17.9096 11.6859 13.6925 16.888 13.6925C17.1467 13.6925 17.4028 13.7038 17.6562 13.7243V6.24121C17.4016 6.22973 17.1455 6.22363 16.888 6.22363C7.56102 6.22363 0 13.7846 0 23.1116C0 32.4386 7.56102 39.9996 16.888 39.9996C26.2149 39.9996 33.7759 32.4386 33.7759 23.1116C33.7759 22.8541 33.7698 22.598 33.7584 22.3433H26.2753C26.2958 22.5968 26.307 22.8529 26.307 23.1116Z" fill="currentColor"></path>
                        </g>
                        <g class="text-primary">
                          <path d="M40 20C40 8.9543 31.0457 0 20 0V20H40Z" fill="currentColor"></path>
                        </g>
                      </svg>
                      <h3 class="h4">Login</h3>
                      <p class="mb-0">Alrready have an account? Do log in</p>
                    </div>
                    <div class="card-footer border-0 pt-3 mb-3"><router-link class="btn btn-icon btn-lg btn-outline-primary rounded-circle stretched-link" to="/account/login"><i class="ai-arrow-right"></i></router-link></div>
                  </div>
                </div>
                <!-- Item-->
                <div class="col">
                  <div class="card h-100 border-0 rounded-5">
                    <div class="card-body pb-3">
                      <svg class="d-block mt-1 mt-sm-0 mb-4" width="40" height="40" viewBox="0 0 40 40" xmlns="http://www.w3.org/2000/svg">
                        <g class="text-info">
                          <path d="M25,25h15v15H25V25z" fill="currentColor"></path>
                        </g>
                        <g class="text-primary">
                          <path d="M10,20c0-5.5,4.5-10,10-10s10,4.5,10,10h10C40,9,31,0,20,0S0,9,0,20s9,20,20,20V30C14.5,30,10,25.5,10,20z" fill="currentColor"></path>
                        </g>
                      </svg>
                      <h3 class="h4">Sign Up</h3>
                      <p class="mb-0">Don't have an account? do sign up'.</p>
                    </div>
                    <div class="card-footer border-0 pt-3 mb-3"><router-link class="btn btn-icon btn-lg btn-outline-primary rounded-circle stretched-link" to="/account/signup"><i class="ai-arrow-right"></i></router-link></div>
                  </div>
                </div>
               
            
            </div>
            <!-- Copyright-->
            <p class="w-100 fs-sm pt-5 mt-auto mb-5" style="max-width: 526px;"><span class="text-muted">thecoffeeroom.in</span></p>

                <div class="w-50 bg-size-cover bg-repeat-0 bg-position-center" style="background-image: url(/assets/images/covers/login.jpg);"></div>
            </div>
                        `,
    data() {
        return {

        };
    },
    async mounted() {
        window.scrollTo({
            top: 0,
            behavior: 'smooth',
        });
    },
    methods: {

    }
};

const LoginComponent = {
    template: `
      <a class="text-nav btn btn-icon bg-light border rounded-circle position-absolute top-0 end-0 p-0 mt-3 me-3 mt-sm-4 me-sm-4" href="/" data-bs-toggle="tooltip" data-bs-placement="left" title="Back to home"><i class="ai-home"></i></a>
        <div class="d-flex flex-column align-items-center w-lg-50 h-100 px-3 px-lg-5 pt-5 fade-in-smooth-pop">
        <div class="w-100 mt-auto" style="max-width: 526px;">
            <h1>Log In</h1>
            <router-link to='/account'>back to menu</router-link>
            <form class="needs-validation" id="loginForm" novalidate>
                <div class="pb-3 mb-3">
                    <div class="position-relative">
                        <i class="ai-mail fs-lg position-absolute top-50 start-0 translate-middle-y ms-3"></i>
                                <input class="form-control form-control-lg ps-5" v-model="username" @@keyup.enter="submitLogin" type="text" placeholder="Username or email" required>
                    </div>
                </div>
                <div class="mb-4">
                    <div class="position-relative">
                        <i class="ai-lock-closed fs-lg position-absolute top-50 start-0 translate-middle-y ms-3"></i>
                        <div class="password-toggle">
                                    <input class="form-control form-control-lg ps-5" v-model="password" @@keyup.enter="submitLogin" type="password" placeholder="Password" required>
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
                    <button class="btn btn-lg btn-primary w-100 mb-4 fade-in-delay" type="button" v-on:click="submitLogin"><span v-html="buttonText"></span></button>

                <h2 class="h6 text-center pt-3 pt-lg-4 mb-4">Or sign in with your social account(id)</h2>
                <div class="row row-cols-1 row-cols-sm-2 gy-3">
                    <div class="col"><a class="btn btn-icon btn-outline-secondary btn-google btn-lg w-100 disabled" href="#"><i class="ai-google fs-xl me-2"></i>Google</a></div>
                    <div class="col"><a class="btn btn-icon btn-outline-secondary btn-facebook btn-lg w-100 disabled" href="#"><i class="ai-facebook fs-xl me-2"></i>Facebook</a></div>
                </div>
            </form>
        </div>
        <!-- Copyright-->
        <p class="w-100 fs-sm pt-5 mt-auto mb-5" style="max-width: 526px;"><span class="text-muted">thecoffeeroom.in</span></p>

            <div class="w-50 bg-size-cover bg-repeat-0 bg-position-center" style="background-image: url(/assets/images/covers/login.jpg);"></div>
        </div>
                    `,
    data() {
        return {
            buttonText: 'Log In',
            username: '',
            password: '',
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
            this.buttonText = '<span class="spinner-grow spinner-grow-sm me-2" role="status" aria-hidden="true"></span>Logging In...';
            const details = {
                UserName: this.username,
                PassWord: this.password,
            };
            const token = document.querySelector('input[name="__RequestVerificationToken"]').value;
            axios.defaults.headers.common['RequestVerificationToken'] = token;
            axios.post('/api/account/login', details)
                .then((response) => {
                    toaster("success", "Logging in..");
                    window.location.href = localStorage.getItem("curr_link");
                })
                .catch((error) => {
                    toaster("error", error.response.data);
                })
                .finally(() => {
                    this.buttonText = 'Log In';
                });
        },
    }
};

const SignupComponent = {
    template: `
            <a class="text-nav btn btn-icon bg-light border rounded-circle position-absolute top-0 end-0 p-0 mt-3 me-3 mt-sm-4 me-sm-4" href="/" data-bs-toggle="tooltip" data-bs-placement="left" title="Back to home"><i class="ai-home"></i></a>

        <div class="d-flex flex-column align-items-center w-lg-50 h-100 px-3 px-lg-5 pt-5 fade-in-smooth-pop">
            <div class="w-100 mt-auto" style="max-width: 526px;">
                <h1>Sign Up</h1>
                    <router-link to='/account'>back to menu</router-link>
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
                        <div class="position-relative pb-4">
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
                                <input name="password" type="password" v-model="passconfirm" class="bindentersignup form-control form-control-lg ps-5 bindenter" placeholder="Confirm Pass" maxlength="20" />
                                <label class="password-toggle-btn" aria-label="Show/hide password">
                                    <input class="password-toggle-check" type="checkbox"><span class="password-toggle-indicator"></span>
                                </label>
                            </div>
                        </div>
                    </div>
                                <button class="btn btn-lg btn-primary w-100 mb-4 fade-in-delay" type="button"  v-on:click="submitLogin"><span v-html="buttonText"></span></button>
                </form>
            </div>
            <!-- Copyright-->
            <p class="w-100 fs-sm pt-5 mt-auto mb-5" style="max-width: 526px;"><span class="text-muted">thecoffeeroom.in</span></p>
                    `,

    data() {
        return {
            buttonText: 'Sign Up',
            username: '',
            password: '',
            passcofirm: '',
            firstname: '',
            lastname: '',
            email: '',

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
            this.buttonText = '<span class="spinner-grow spinner-grow-sm me-2" role="status" aria-hidden="true"></span>Signing Up...';
            const details = {
                FirstName: this.firstname,
                LastName: this.lastname,
                UserName: this.username,
                Password: this.password,
                EMail: this.email
            };
            const token = document.querySelector('input[name="__RequestVerificationToken"]').value;
            axios.defaults.headers.common['RequestVerificationToken'] = token;
            axios.post('/api/account/signup', details)
                .then((response) => {
                    console.log(response.data);
                    toaster("success", response.data.message);
                })
                .catch((error) => {
                    toaster("error", error.response.data);
                })
                .finally(() => {
                    this.buttonText = 'Sign Up';
                    this.lala = 'Sign Up';
                });
        },
    },
};

const PasswordReset = {
    template: `
          <a class="text-nav btn btn-icon bg-light border rounded-circle position-absolute top-0 end-0 p-0 mt-3 me-3 mt-sm-4 me-sm-4" href="/" data-bs-toggle="tooltip" data-bs-placement="left" title="Back to home"><i class="ai-home"></i></a>
            <div class="d-flex flex-column align-items-center w-lg-50 h-100 px-3 px-lg-5 pt-5 fade-in">
            <div class="w-100 mt-auto" style="max-width: 526px;">
                <h1>Log In</h1>
                <p class="pb-3 mb-3 mb-lg-4">Don't have an account yet?&nbsp;&nbsp;<router-link to='/account/signup'>Register here!</router-link></p>
                <form class="needs-validation" id="passReset" novalidate>
                    <div class="pb-3 mb-3">
                        <div class="position-relative">
                            <i class="ai-mail fs-lg position-absolute top-50 start-0 translate-middle-y ms-3"></i>
                            <input class="form-control form-control-lg ps-5" v-model="email" @@keyup.enter="submitLogin" type="text" placeholder="Username or email" required>
                        </div>
                    </div>
                    <div class="mb-4">
                        <div class="position-relative">
                            <i class="ai-lock-closed fs-lg position-absolute top-50 start-0 translate-middle-y ms-3"></i>
                            <div class="password-toggle">
                                        <input class="form-control form-control-lg ps-5" v-model="password" @@keyup.enter="submitLogin" type="password" placeholder="Password" required>
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
                        <button class="btn btn-lg btn-primary w-100 mb-4" type="button" v-on:click="submitLogin"><span v-html="buttonText"></span></button>

                    <h2 class="h6 text-center pt-3 pt-lg-4 mb-4">Or sign in with your social account(id)</h2>
                    <div class="row row-cols-1 row-cols-sm-2 gy-3">
                        <div class="col"><a class="btn btn-icon btn-outline-secondary btn-google btn-lg w-100 disabled" href="#"><i class="ai-google fs-xl me-2"></i>Google</a></div>
                        <div class="col"><a class="btn btn-icon btn-outline-secondary btn-facebook btn-lg w-100 disabled" href="#"><i class="ai-facebook fs-xl me-2"></i>Facebook</a></div>
                    </div>
                </form>
            </div>
            <!-- Copyright-->
            <p class="w-100 fs-sm pt-5 mt-auto mb-5" style="max-width: 526px;"><span class="text-muted">thecoffeeroom.in</span></p>

                <div class="w-50 bg-size-cover bg-repeat-0 bg-position-center" style="background-image: url(/assets/images/covers/login.jpg);"></div>
            </div>
                        `,
    data() {
        return {
            buttonText: 'Log In',
            username: '',
            password: '',
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
            this.buttonText = '<span class="spinner-grow spinner-grow-sm me-2" role="status" aria-hidden="true"></span>Logging In...';
            const details = {
                UserName: this.username,
                PassWord: this.password,
            };
            const token = document.querySelector('input[name="__RequestVerificationToken"]').value;
            axios.defaults.headers.common['RequestVerificationToken'] = token;
            axios.post('/api/account/login', details)
                .then((response) => {
                    toaster("success", "Logging in..");
                    window.location.href = localStorage.getItem("curr_link");
                })
                .catch((error) => {
                    toaster("error", error.response.data);
                })
                .finally(() => {
                    this.buttonText = 'Log In';
                });
        },
    }
};

const NewPassword = {
    template: `
              <a class="text-nav btn btn-icon bg-light border rounded-circle position-absolute top-0 end-0 p-0 mt-3 me-3 mt-sm-4 me-sm-4" href="/" data-bs-toggle="tooltip" data-bs-placement="left" title="Back to home"><i class="ai-home"></i></a>
                <div class="d-flex flex-column align-items-center w-lg-50 h-100 px-3 px-lg-5 pt-5 fade-in-pop">
                <div class="w-100 mt-auto" style="max-width: 526px;">
                    <h1>Password Reset</h1>
                    <form class="needs-validation" id="passReset" novalidate>
                        <div class="pb-3 mb-3">
                            <div class="position-relative">
                                <i class="ai-mail fs-lg position-absolute top-50 start-0 translate-middle-y ms-3"></i>
                                        <input class="form-control form-control-lg ps-5" v-model="newpass" @@keyup.enter="submitLogin" type="text" placeholder="Username or email" required>
                            </div>
                        </div>
                        <div class="mb-4">
                            <div class="position-relative">
                                <i class="ai-lock-closed fs-lg position-absolute top-50 start-0 translate-middle-y ms-3"></i>
                                <div class="password-toggle">
                                            <input class="form-control form-control-lg ps-5" v-model="password" @@keyup.enter="submitLogin" type="password" placeholder="Password" required>
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
                            </form-check><router-link class="fs-sm fw-semibold text-decoration-none my-1" href="/account/password-recovery">Forgot password?</a>
                        </div>
                            <button class="btn btn-lg btn-primary w-100 mb-4" type="button" v-on:click="submitLogin"><span v-html="buttonText"></span></button>

                        <h2 class="h6 text-center pt-3 pt-lg-4 mb-4">Or sign in with your social account(id)</h2>
                        <div class="row row-cols-1 row-cols-sm-2 gy-3">
                            <div class="col"><a class="btn btn-icon btn-outline-secondary btn-google btn-lg w-100 disabled" href="#"><i class="ai-google fs-xl me-2"></i>Google</a></div>
                            <div class="col"><a class="btn btn-icon btn-outline-secondary btn-facebook btn-lg w-100 disabled" href="#"><i class="ai-facebook fs-xl me-2"></i>Facebook</a></div>
                        </div>
                    </form>
                </div>
                <!-- Copyright-->
                <p class="w-100 fs-sm pt-5 mt-auto mb-5" style="max-width: 526px;"><span class="text-muted">thecoffeeroom.in</span></p>

                    <div class="w-50 bg-size-cover bg-repeat-0 bg-position-center" style="background-image: url(/assets/images/covers/login.jpg);"></div>
                </div>
                            `,
    data() {
        return {
            buttonText: 'Log In',
            username: '',
            password: '',
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
            this.buttonText = '<span class="spinner-grow spinner-grow-sm me-2" role="status" aria-hidden="true"></span>Logging In...';
            const details = {
                UserName: this.username,
                PassWord: this.password,
            };
            const token = document.querySelector('input[name="__RequestVerificationToken"]').value;
            axios.defaults.headers.common['RequestVerificationToken'] = token;
            axios.post('/api/account/login', details)
                .then((response) => {
                    toaster("success", "Logging in..");
                    window.location.href = localStorage.getItem("curr_link");
                })
                .catch((error) => {
                    toaster("error", error.response.data);
                })
                .finally(() => {
                    this.buttonText = 'Log In';
                });
        },
    }
};
const routes = [
    {
        path: '/account/',
        component: BaseComponent
    }, {
        path: '/account/login',
        component: LoginComponent
    }, {
        path: '/account/signup',
        component: SignupComponent
    }, {
        path: '/account/recover-password',
        component: PasswordReset
    }, {
        path: '/account/new-password',
        component: NewPassword
    }
];
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
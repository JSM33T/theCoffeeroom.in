  <template>
<div class="fade-in">
    <div class="d-flex align-items-center mt-sm-n1 pb-4 mb-0 mb-lg-1 mb-xl-3">
        <i class="ai-edit text-primary lead pe-1 me-2"></i>
        <h2 class="h4 mb-0">Edit Info</h2><div class="ms-auto"><router-link class="btn btn-sm btn-secondary ripple" to="/profile"><i class="ai-user ms-n1 me-2"></i>Dashboard</router-link><router-link class="btn btn-sm btn-secondary ripple mx-2" to="/profile/security"><i class="ai-user ms-n1 me-2"></i>Security</router-link></div>
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
                    <option v-for="option in options" :data-id="option.id" :value="option.image" :key="option.title">{{ option.title }}</option>
                </select>
            </div>
        </div>
    </div>
    <div class="row g-3 g-sm-4 mt-0 mt-lg-2">
        <div class="col-sm-6">
            <label class="form-label" for="fn">First name</label>
            <input type="text" v-model="firstname" class="form-control " aria-autocomplete="none" value="@Model.FirstName" maxlength="20" />
        </div>
        <div class="col-sm-6">
            <label class="form-label" for="ln">Last name</label>
            <input type="text" v-model="lastname" class="form-control " aria-autocomplete="none" value="@Model.LastName" maxlength="20" />
        </div>
        <div class="col-sm-6">
            <label class="form-label" for="email">Email address</label>
            <input type="text" v-model="email" class="form-control" placeholder="Enter your email" value="@Model.EMail" aria-autocomplete="none" maxlength="40" />
        </div>
        <div class="col-sm-6">
            <label class="form-label" for="phone">Phone <span class='text-muted'>(optional)</span></label>
            <input v-model="phone" type="text" id="phone" class="form-control" value="@Model.Phone" placeholder="" maxlength="15" />
        </div>
        <div class="col-12">
            <label class="form-label" for="phone">Bio <span class='text-muted'>(optional)</span></label>
            <textarea v-model="bio" type="text" id="bio" class="form-control" placeholder="enter a bio" maxlength="100">@Model.Bio</textarea>
        </div>

        <div class="col-sm-12 col-md-6  d-sm-flex align-items-center pt-sm-2 pt-md-3">
            <div class="form-label text-muted mb-2 mb-sm-0 me-sm-4">Gender:</div>
            <div class="form-check form-check-inline mb-0">
                <input value="m" v-model="gender" type="radio" id="male" class="form-check-input" @(@Model.Gender= ="m" ? "checked" : "false" ) />
                <label class="form-check-label" for="male">Male</label>
            </div>
            <div class="form-check form-check-inline mb-0">
                <input value="f" v-model="gender" type="radio" id="female" class="form-check-input" @(@Model.Gender= ="f" ? "checked" : "false" ) />
                <label class="form-check-label" for="female">Female</label>
            </div>
            <div class="form-check form-check-inline mb-0">
                <input value="o" v-model="gender" type="radio" id="other" class="form-check-input" @(@Model.Gender= ="o" ? "checked" : "false" ) />
                <label class="form-check-label" for="other">Other</label>
            </div>
        </div>
        <div class="col-12 d-flex justify-content-end pt-3">
            <button class="btn btn-primary ms-3 ripple" type="button" id="saveprof" @@click="saveDetails">Save Profile</button>
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
    role: "@userrole",
    someThing : "2"
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
    selectValue(){
    this.$nextTick(() => {
    document.getElementById("avatars").value = '@Context.Session.GetString("avatar")';
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

    const data={
    firstname: this.firstname,
    lastname:this.lastname,
    bio: this.bio,
    phone: this.phone,
    gender: this.gender,
    avatarId: avtId,
    };
    axios.post("/api/profile/update",data)
    .then(response => {
    console.log(response.data);
    document.getElementById('layout_pfp').src = '/assets/images/avatars/default/' + avtVal + '.png';
    toaster("success",response.data);
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
    //  alert(selectedOptionValue);
    //     document.getElementById('avatar_placeholder').style.background-image = 'url(/assets/images/avatars/default/default.png)';
    document.getElementById('avatar_placeholder').style.backgroundImage = 'url(/assets/images/avatars/default/' + selectedOptionValue + '.png)';



    },
    //saveDetails(){
    //    toaster("success","save changes event triggered");
    //}
    },

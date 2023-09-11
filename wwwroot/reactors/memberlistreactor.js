const MemberDashboard = {
    props: ['param2'],
    setup(props) {
        const { param2 } = Vue.toRefs(props);
        const title = Vue.ref("Home");
        const endPoint = Vue.ref("");
        const userName = Vue.ref("");
        const firstName = Vue.ref("");
        const lastName = Vue.ref("");
        const phone = Vue.ref("");
        const bio = Vue.ref("");    
        const avatar = Vue.ref("");
        const role = Vue.ref("");
        const endPoint = Vue.ref("");
        const isLoading = Vue.ref(true);
        //const getBadgeClass = (badge) => {

        //    badgeval = badge.trim().toLowerCase();
        //    console.log(badgeval);
        //    if (badgeval === 'contributor') {
        //        return 'bg-faded-success text-success';
        //    } else if (badgeval === 'vip') {
        //        return 'bg-faded-warning text-warning';
        //    } else if (badgeval === 'founder') {
        //        return 'bg-faded-info text-danger';
        //    } else {
        //        return 'bg-faded-primary text-primary';
        //    }

        //};
        const fetchBadges = async () => {
            try {
                startProg();
                endPoint.value = param2.value;
                const response = await axios.get('/api/members/getmembers/' + endPoint.value);
                members.value = response.data;
                console.log('/api/members/getmembers/' + param1.value);
                console.log(response.data);
            } catch (error) {
                console.error('Error fetching user badges:', error);
            }
            finally {
                endProg();
            }
        };
        const fetchMemberData = async () => {
                await axios.get("/api/member/getdetails/12")
                    .then(response => {
                        console.log(response.data);
                        username.value = response.data.userName;
                        firstname.value = response.data.firstName;
                        lastname.value = response.data.lastName;
                        phone.value = response.data.phone;
                        bio.value = response.data.bio;
                        avatars.value = response.data.avatarImg;
                        avatar.value = response.data.avatarImg + ".png";
                        gender.value = response.data.gender;
                        role.value = response.data.role;

                    })
                    .catch(error => {
                        console.log(error);
                        //toaster("error", "something went wrong");
                        toaster("error", error.response.data);
                    });
        }


      
        Vue.onMounted(() => {
            isLoading.value = false;
            fetchMemberData();
        });

        return {
            param2,
            title,
            isLoading,
            fetchMemberData,
            fetchBadges,


        };
    },

    template: `
       
        <div v-if="isLoading">
            Loading...
        </div>
        <div v-else>
            <div class="navbar card-header w-100">
                <h1 class="fade-in px-4" id="sectionTitle" class="h2 ">
                      {{param2}}
                </h1>
                <div>
                    <router-link :to="'/members/'" class="btn btn-sm btn-secondary ripple my-1 mx-1"><i class="ai-user ms-n1 me-2"></i>Back</router-link>
                   
                   
                </div>
            </div>
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
};

const MembersComp = {
    props: ['param1'],
    setup(props) {
        const { param1 } = Vue.toRefs(props);
        const title = Vue.ref("Home");
        const members = Vue.ref([]);
        const endPoint = Vue.ref("");
        const isLoading = Vue.ref(true);
        const getBadgeClass = (badge) => {
           
            badgeval = badge.trim().toLowerCase();
            console.log(badgeval);
            if (badgeval === 'contributor') {
                return 'bg-faded-success text-success';
            } else if (badgeval === 'vip') {
                return 'bg-faded-warning text-warning';
            } else if (badgeval === 'founder') {
                return 'bg-faded-info text-danger';
            } else {
                return 'bg-faded-primary text-primary';
            }
           
        };
        const fetchMembers = async () => {
            try {
                startProg();
                endPoint.value = param1.value === "" ? "all" : param1.value;
                const response = await axios.get('/api/members/getmembers/' + endPoint.value);
                members.value = response.data;
                console.log('/api/members/getmembers/' + param1.value);
                console.log(response.data);
            } catch (error) {
                console.error('Error fetching user badges:', error);
            }
            finally {
                endProg();
            }
        }

        const loadPosts = () => {
            console.log("posts loaded");
        }
        Vue.onMounted(() => {
            title.value = param1.value === "" ? "all" : param1.value;
            fetchMembers();
            isLoading.value = false;
            
        });

        Vue.watch(param1, (newVal, oldVal) => {
            title.value = newVal === undefined || newVal === "" ? "all" : newVal;
            fetchMembers();
        });

        return {
            param1,
            title,
            isLoading,
            members,
            fetchMembers,
            getBadgeClass


        };
    },

    template: `
       
        <div v-if="isLoading">
            Loading...
        </div>
        <div v-else>
            <div class="navbar card-header w-100">
                <h1 class="fade-in px-4" id="sectionTitle" class="h2 ">
                     {{ title === 'all' ? title : title + 's'}}
                </h1>
                <div>
                    <router-link :to="'/members/'" class="btn btn-sm btn-secondary ripple my-1 mx-1"><img src="/assets/svg/badges/badges.svg"  width="20" >&nbsp;Badged Members</router-link>
                    <router-link :to="'/members/pioneer'" class="btn btn-sm btn-secondary ripple my-1 mx-1"><img src="/assets/svg/badges/pioneer.svg"  width="20" >&nbsp;Pioneers</router-link>
                    <router-link :to="'/members/contributor'" class=" btn btn-sm btn-secondary my-1 ripple mx-1"><img src="/assets/svg/badges/contributor.svg"  width="20" >&nbsp;Contributors</router-link>
                    <router-link :to="'/members/founder'" class="btn btn-sm btn-secondary ripple my-1 mx-1"><img src="/assets/svg/badges/founder.svg"  width="20" >&nbsp;Founders</router-link>
                    <router-link :to="'/members/vip'" class="btn btn-sm btn-secondary ripple my-1 mx-1"><img src="/assets/svg/badges/vip.svg"  width="20" >&nbsp;VIP</router-link>
                </div>
            </div>
            <div  class="fade-in row mt-2">
                 <div v-for="member in members" :key="member.userName" class="col-md-6 pb-2 pt-2">
                    <div class="card h-100 rounded-3 p-3 p-sm-4">
                      <div class="d-flex align-items-center pb-2 mb-1">
                        <h3 class="h6 text-nowrap text-truncate mb-0"><router-link :to="'/member/' + member.userName ">@{{ member.userName }}</router-link></h3>
                        <span class="badge fs-xs ms-1"  :class="getBadgeClass(badge)" v-for="badge in member.badges.split(',')" :key="badge">{{ badge }}</span>
                        <div class="d-flex ms-auto">
                        <div class="mx-1" v-for="badge in member.badges.split(',')" :key="badge">
                          <img :src="'/assets/svg/badges/'+ badge.trim().toLowerCase() +'.svg'" width="22">
                        </div>
                        </div>
                      </div>
                      <div class="d-flex align-items-center"><img width="50" style="border-radius:30px" :src="'/assets/images/avatars/default/' + member.avatarImg + '.png'">
                        <div class="ps-3 fs-sm">
                          <div class="text-dark">{{member.firstName}} {{member.lastName}}</div>
                          <div class="text-muted">#{{member.id}}</div>
                        </div>
                      </div>
                    </div>
                  </div>
            </div>
        </div>
                        
    `,
};



const routes = [
    {
        path: '/members/:param1?',
        component: MembersComp,
        props: true,
        watchQuery: ['param1']
    },
     {
        path: '/member/:param2?',
        component: MemberDashboard,
        props: true,
        watchQuery: ['param2']
    }
];

const router = VueRouter.createRouter({
    history: VueRouter.createWebHistory(),
    routes,
});

const app = Vue.createApp({

});

app.use(router);
app.mount('#appMembers');


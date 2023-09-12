﻿
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
            if (badgeval === 'contributors') {
                return 'bg-faded-primary text-primary';
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
                    <router-link :to="'/members/'" class="btn btn-sm btn-secondary ripple my-1 mx-1"><i class="ai-user ms-n1 me-2"></i>Badged Members</router-link>
                    <router-link :to="'/members/pioneer'" class="btn btn-sm btn-secondary ripple my-1 mx-1"><i class="ai-user ms-n1 me-2"></i>Pioneers</router-link>
                    <router-link :to="'/members/contributor'" class=" btn btn-sm btn-secondary my-1 ripple mx-1"><i class="ai-user ms-n1 me-2"></i>Contributors</router-link>
                    <router-link :to="'/members/founder'" class="btn btn-sm btn-secondary ripple my-1 mx-1"><i class="ai-user ms-n1 me-2"></i>Founders</router-link>
                    <router-link :to="'/members/vip'" class="btn btn-sm btn-secondary ripple my-1 mx-1"><i class="ai-user ms-n1 me-2"></i>VIP</router-link>
                    
                   
                </div>
            </div>
            <div  class="fade-in row mt-2">
         
                 <div v-for="member in members" :key="member.userName" class="col-md-6 pb-2 pt-2">
                    <div class="card h-100 rounded-3 p-3 p-sm-4">
                      <div class="d-flex align-items-center pb-2 mb-1">
                        <h3 class="h6 text-nowrap text-truncate mb-0"><a :href="'/member/' + member.userName ">@{{ member.userName }}</a></h3>
                        
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
        path: '/member/:param1',
        component: MemberDashBoard
    }
     {
        path: '/members/',
        component: MemberDashBoard
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


const WallPostsFiltered = {
    props: ['param1'],
    setup(props) {
        const { param1 } = Vue.toRefs(props);
        const title = Vue.ref("Home");

        Vue.onMounted(() =>
        {
            title.value = param1.value === "" ? "#all" : "#" + param1.value;
        });

        Vue.watch(param1, (newVal, oldVal) =>
        {
            title.value = newVal === undefined || newVal === "" ? "#all" : "#" + newVal;
        });

        return {
            param1,
            title,
            isLoading: Vue.ref(false),
        };
    },

    template: `
       
        <div v-if="isLoading">
            Loading...
        </div>
        <div v-else>
            <div class="navbar card-header w-100">
                <h1 class="fade-in px-4" id="sectionTitle" class="h2 ">{{title}}</h1>
                <div>
                    <router-link :to="'/wall'" class="btn btn-sm btn-secondary ripple mx-1"><i class="ai-user ms-n1 me-2"></i>#all</router-link>
                    <router-link :to="'/wall/bug'" class="btn btn-sm btn-secondary ripple mx-1"><i class="ai-user ms-n1 me-2"></i>#bug</router-link>
                    <router-link :to="'/wall/feedback'" class="btn btn-sm btn-secondary ripple mx-1"><i class="ai-user ms-n1 me-2"></i>#feedback</router-link>
                    <router-link :to="'/wall/admin'" class=" btn btn-sm btn-secondary ripple mx-1"><i class="ai-user ms-n1 me-2"></i>#admin</router-link>
                </div>
            </div>
            <!-- Body-->
            <div class="card-body pb-0 fade-in" data-simplebar style="max-height: 580px;">
                <div class="text-muted text-center mb-4">May 27, 2022</div>
                <!-- Message-->
                <div class="mb-3" style="max-width: 392px;">
                    <div class="d-flex align-items-end mb-2">
                        <div class="flex-shrink-0 pe-2 me-1"><img class="rounded-circle" src="/assets/img/avatar/19.jpg" width="48" alt="Avatar"></div>
                        <div class="message-box-start text-dark">Thank you for recommending me as a designer. I have an interview tomorrow!</div>
                    </div>
                    <div class="fs-xs text-muted text-end">11:32 am</div>
                </div>
                <!-- Message-->
                <div class="ms-auto mb-3" style="max-width: 392px;">
                    <div class="d-flex align-items-end mb-2">
                        <div class="message-box-end bg-primary text-white">Oh no thanks! I just know that you are a great specialist!</div>
                        <div class="flex-shrink-0 ps-2 ms-1"><img class="rounded-circle" src="/assets/img/avatar/01.jpg" width="48" alt="Avatar"></div>
                    </div>
                    <div class="fs-xs text-muted"><i class="ai-checks text-primary fs-xl mt-n1 me-1"></i>2:18 pm</div>
                </div>
                <div class="text-muted text-center my-4">May 29, 2022</div>
                <div class="mb-3" style="max-width: 392px;">
                    <div class="d-flex align-items-end mb-2">
                        <div class="flex-shrink-0 pe-2 me-1"><img class="rounded-circle" src="/assets/img/avatar/19.jpg" width="48" alt="Avatar"></div>
                        <div class="w-100">
                            <div class="message-box-start text-dark mb-2">I have great news, I've been hired! 🚀</div>
                            <div class="message-box-start text-dark">Thanks again!</div>
                        </div>
                    </div>
                    <div class="fs-xs text-muted text-end">4:04 am</div>
                </div>
                <div class="ms-auto mb-3" style="max-width: 392px;">
                    <div class="d-flex align-items-end mb-2">
                        <div class="message-box-end bg-primary text-white">You are welcome!</div>
                        <div class="flex-shrink-0 ps-2 ms-1"><img class="rounded-circle" src="/assets/img/avatar/01.jpg" width="48" alt="Avatar"></div>
                    </div>
                    <div class="fs-xs text-muted"><i class="ai-check text-primary fs-xl mt-n1 me-1"></i>2:18 pm</div>
                </div>
                <div class="text-muted text-center my-4">Today</div>
                <div class="mb-3" style="max-width: 392px;">
                    <div class="d-flex align-items-end mb-2">
                        <div class="flex-shrink-0 pe-2 me-1"><img class="rounded-circle" src="/assets/img/avatar/19.jpg" width="48" alt="Avatar"></div>
                        <div  class="message-box-start text-dark">I'm so happy to be part of the team and work with you on this new exciting project. Can't thank you enough 😊</div>
                    </div>
                    <div class="fs-xs text-muted text-end">10:17 am</div>
                </div>
            </div>


            <div class="card-footer w-100 border-0 mx-0 px-4">
                <div class="d-flex align-items-end border rounded-3 pb-3 pe-3 mx-sm-3">
                    <textarea class="form-control border-0" rows="3" placeholder="Type a message" style="resize: none;"></textarea>
                    <div class="nav flex-nowrap align-items-center">
                        <a class="nav-link text-muted p-1 me-1"><i class="ai-paperclip fs-xl"></i></a><a class="nav-link text-muted p-1" href="#"><i class="ai-emoji-smile fs-xl"></i></a>
                        <button href="#" class="btn btn-sm btn-secondary ms-3" type="button">Send</button>
                    </div>
                </div>
            </div>
        </div>
                        
    `,
};

const routes = [
    {
        path: '/wall/:param1?',
        component: WallPostsFiltered,
        props: true,
        watchQuery: ['param1']
    },
];

const router = VueRouter.createRouter({
    history: VueRouter.createWebHistory(),
    routes,
});

const app = Vue.createApp({
    
});

app.use(router);
app.mount('#appWall');

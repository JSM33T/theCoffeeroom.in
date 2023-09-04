const WallPostsFiltered = {
    props: ['param1'],
    setup(props) {
        const param1 = Vue.ref(props.param1);
        return {
            param1,
            isLoading: Vue.ref(false),
        };
        Vue.onMounted(() => {
            console.log('Component is mounted!');
        });
    },

    template: `
        <div v-if="isLoading">
          Loading...
        </div>
        <div v-else>
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
                                        <!-- Message-->
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
                                        <!-- Message-->
                                        <div class="ms-auto mb-3" style="max-width: 392px;">
                                            <div class="d-flex align-items-end mb-2">
                                                <div class="message-box-end bg-primary text-white">You are welcome!</div>
                                                <div class="flex-shrink-0 ps-2 ms-1"><img class="rounded-circle" src="/assets/img/avatar/01.jpg" width="48" alt="Avatar"></div>
                                            </div>
                                            <div class="fs-xs text-muted"><i class="ai-check text-primary fs-xl mt-n1 me-1"></i>2:18 pm</div>
                                        </div>
                                        <div class="text-muted text-center my-4">Today</div>
                                        <!-- Message-->
                                        <div class="mb-3" style="max-width: 392px;">
                                            <div class="d-flex align-items-end mb-2">
                                                <div class="flex-shrink-0 pe-2 me-1"><img class="rounded-circle" src="/assets/img/avatar/19.jpg" width="48" alt="Avatar"></div>
                                                <div class="message-box-start text-dark">I'm so happy to be part of the team and work with you on this new exciting project. Can't thank you enough 😊</div>
                                            </div>
                                            <div class="fs-xs text-muted text-end">10:17 am</div>
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
    // You can add any global app-level logic here.
});

app.use(router);
app.mount('#appWall');
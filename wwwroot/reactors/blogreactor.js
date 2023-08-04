const routerViewElement = document.querySelector('router-view');
const CategorisedBlogComponent = {
    props: ['param1', 'param2'],
    watch: {
        // Watch for changes in route parameters
        $route(to, from) {
            if (to.params.param1 !== from.params.param1 || to.params.param2 !== from.params.param2) {
                routerViewElement.innerHTML =
                    this.blogs = [];
                this.$nextTick(() => {
                    this.loadDefault();
                    window.scrollTo({
                        top: 0,
                        behavior: 'smooth', // Use 'smooth' for smooth scrolling effect, 'auto' for instant scrolling
                    });
                });
            }
        }
    },
    mounted() {
        isLoading: false,
            this.loadDefault("", ""),

            window.scrollTo({
                top: 0,
                behavior: 'smooth', // Use 'smooth' for smooth scrolling effect, 'auto' for instant scrolling
            });
    },

    methods: {

        async loadDefault() {
            document.getElementById("titleBlog").innerHTML = "Blogs '" + this.param2 + "'";
            try {
                const response = await axios.get('/api/blogs/0/' + this.param1 + '/' + this.param2);
                const data = response.data;
                this.blogs = data;
            } catch (error) {
                console.error('Error fetching data from API:', error);
            } finally {

            }
        },

    },
    data() {
        return {
            blogs: [], // We will store the fetched data here,
            isLoading: true,
            //titleItem: "Blogs -" + this.param2

        };
    },
    template: `
                 <div v-for="blog in blogs" :key="blog.title">
                <article class="row g-0 border-0 mb-4">
                    <a class="col-sm-5 bg-repeat-0 bg-size-cover bg-position-center rounded-5"
                       v-bind:href="'/blog/' + blog.datePosted.substring(0,4) + '/' + blog.urlHandle "
                       v-bind:style="{ 'background-image': 'url(/content/blogcontent/' + blog.datePosted.substring(0, 4) + '/' + blog.urlHandle + '/cover.jpg)', 'min-height': '14rem' }">
                    </a>
                    <div class="col-sm-7">
                        <div class="pt-4 pb-sm-4 ps-sm-4 pe-lg-4">
                            <h3>
                                <router-link v-bind:href="'/blog/' + blog.datePosted.substring(0, 4)+'/' + blog.urlHandle">
                                    {{blog.title}}</router-link>
                            </h3>
                            <p class="d-sm-none d-md-block">{{blog.description}}</p>
                            <div class="d-flex flex-wrap align-items-center mt-n2">
                                <a class="nav-link text-muted fs-sm fw-normal d-flex align-items-end p-0 mt-2" href="#">{{blog.comments}}<i class="ai-message fs-lg ms-1"></i></a>
                                <span class="fs-xs opacity-20 mt-2 mx-3">|</span>
                                <span class="fs-sm text-muted mt-2">{{blog.datePosted.substring(0, 7)}}</span>
                                <span class="fs-xs opacity-20 mt-2 mx-3">|</span>
                                <router-link class="badge text-nav fs-xs border mt-2" :to="'/blogs/category/'+  blog.locator ">{{blog.category}}</router-link>
                            </div>
                        </div>
                    </div>
                </article>
            </div>
                                `,

};
// Vue.js component for handling the Home route
const HomeComponent = {
    template: `
                  <div v-for="blog in blogs" :key="blog.title">
                <article class="row g-0 border-0 mb-4">
                    <a class="col-sm-5 bg-repeat-0 bg-size-cover bg-position-center rounded-5"
                       v-bind:href="'/blog/' + blog.datePosted.substring(0,4) + '/' + blog.urlHandle "
                       v-bind:style="{ 'background-image': 'url(/content/blogcontent/' + blog.datePosted.substring(0, 4) + '/' + blog.urlHandle + '/cover.jpg)', 'min-height': '14rem' }">
                        <!-- Content inside the anchor tag -->
                    </a>
                    <div class="col-sm-7">
                        <div class="pt-4 pb-sm-4 ps-sm-4 pe-lg-4">
                            <h3>
                                <router-link v-bind:href="'/blog/' + blog.datePosted.substring(0, 4)+'/' + blog.urlHandle">
                                    {{blog.title}}</router-link>
                            </h3>
                            <p class="d-sm-none d-md-block">{{blog.description}}</p>
                            <div class="d-flex flex-wrap align-items-center mt-n2">
                                <a class="nav-link text-muted fs-sm fw-normal d-flex align-items-end p-0 mt-2" href="#">{{blog.comments}}<i class="ai-message fs-lg ms-1"></i></a>
                                <span class="fs-xs opacity-20 mt-2 mx-3">|</span>
                                <span class="fs-sm text-muted mt-2">{{blog.datePosted.substring(0, 7)}}</span>
                                <span class="fs-xs opacity-20 mt-2 mx-3">|</span>
                                <router-link class="badge text-nav fs-xs border mt-2" :to="'/blogs/category/'+  blog.locator ">{{blog.category}}</router-link>
                            </div>
                        </div>
                    </div>
                </article>
            </div>

                                `,
    data() {
        return {
            blogs: [],
            titleItem: 'Blogs'
        };
    },
    async mounted() {
        window.scrollTo({
            top: 0,
            behavior: 'smooth',
        });

        try {
            const response = await axios.get('/api/blogs/0/na/na');
            const data = response.data;
            this.blogs = data;
            console.log(data);
        } catch (error) {
            console.error('Error fetching data from API:', error);
        } finally {

        }
    },
};

const routes = [
    { path: '/blogs', component: HomeComponent },
    { path: '/blogs/:param1/:param2', component: CategorisedBlogComponent, props: true }
];

const router = VueRouter.createRouter({
    history: VueRouter.createWebHistory(),
    routes
});

const app = Vue.createApp({
    data() {
        return {
            isLoading: true,
            inputValue: '',
            titleItem: 'Blogs'
        };
    },
    methods:
    {
        updateRouteParam() {
            if (this.inputValue.trim() !== '') {
                this.$router.replace({ path: `/blogs/search/${this.inputValue}` });
            }
            else {
                this.$router.replace({ path: `/blogs` });
            }
        }
    }
});
app.use(router);
app.mount('#app');
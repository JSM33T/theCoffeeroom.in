const HomeComponent = {
    mounted() {
        this.loadDefault();
    },
    methods: {
        async loadDefault() {
            document.getElementById("titleBlog").innerHTML = "something";
            try {
                const response = await axios.get('/api/livesearch/all/blog');
                const data = response.data;
                this.blogs = data;
            } catch (error) {
                console.error('Error fetching data from API:', error);
            }
        },
    },
    data() {
        return {
            blogs: [] // We will store the fetched data here
        };
    },
    template: `
            <div>
              <ul>
                <li v-for="blog in blogs" :key="blog.title">{{ blog.title }}</li>
              </ul>
            </div>
          `
};

// Vue.js component for handling dynamic params
const DynamicParamsComponent = {
    props: ['param1', 'param2'],
    mounted() {
        this.loadDefault();
    },
    methods: {
        async loadDefault() {
            document.getElementById("titleBlog").innerHTML = "another thing";
            try {
                const response = await axios.get('/api/livesearch/all/blog');
                const data = response.data;
                this.blogs = data;
            } catch (error) {
                console.error('Error fetching data from API:', error);
            }
        },
    },
    data() {
        return {
            blogs: [] // We will store the fetched data here
        };
    },
    template: `
                    <div>
                      <ul>
                        <li v-for="blog in blogs" :key="blog.title">{{ blog.title }}</li>
                      </ul>
                    </div>
                  `
};

// Vue Router configuration
const routes = [
    { path: '/blogs', component: HomeComponent },
    { path: '/blogs/:param1/:param2', component: DynamicParamsComponent, props: true }
];

const router = new VueRouter({
    mode: 'history',
    routes
});

//const app = new Vue({
//    router
//}).$mount('#app');

new Vue({
    router,
}).$mount('#app');
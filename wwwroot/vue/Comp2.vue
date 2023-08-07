<!-- Dynamic Component.vue -->

<template>
    <div>
        <div v-for="blog in blogs" :key="blog.title">
            <article class="row g-0 border-0 mb-4">
                <a class="col-sm-5 bg-repeat-0 bg-size-cover bg-position-center rounded-5"
                   :href="'/blog/' + blog.datePosted.substring(0, 4) + '/' + blog.urlHandle"
                   :style="{ 'background-image': 'url(/content/blogcontent/' + blog.datePosted.substring(0, 4) + '/' + blog.urlHandle + '/cover.jpg)', 'min-height': '14rem' }">
                </a>

                <div class="col-sm-7">
                    <div class="pt-4 pb-sm-4 ps-sm-4 pe-lg-4">
                        <h3><a :href="'/blog/' + blog.datePosted.substring(0, 4) + '/' + blog.urlHandle">{{ blog.title }}</a></h3>
                        <p class="d-sm-none d-md-block">{{ blog.description }}</p>
                        <div class="d-flex flex-wrap align-items-center mt-n2">
                            <a class="nav-link text-muted fs-sm fw-normal d-flex align-items-end p-0 mt-2" href="#">
                                {{ blog.comments }}<i class="ai-message fs-lg ms-1"></i>
                            </a>
                            <span class="fs-xs opacity-20 mt-2 mx-3">|</span>
                            <span class="fs-sm text-muted mt-2">{{ blog.datePosted.substring(0, 7) }}</span>
                            <span class="fs-xs opacity-20 mt-2 mx-3">|</span>
                            <router-link class="badge text-nav fs-xs border mt-2" :to="'/blogs/category/' + blog.locator">
                                {{ blog.category }}
                            </router-link>
                        </div>
                    </div>
                </div>
            </article>
        </div>
    </div>
</template>

<script>
    import axios from 'axios';

    export default {
        data() {
            return {
                blogs: [],
                url: '/blogs/'
            };
        },
        async mounted() {
            routerViewElement.innerHTML = '';
            document.getElementById('loadershit').style.display = 'block';
            document.getElementById('titleBlog').innerHTML = 'another thing';
            try {
                const response = await axios.get('/api/blogs/0/na/na');
                const data = response.data;
                this.blogs = data;
                console.log(data);
            } catch (error) {
                console.error('Error fetching data from API:', error);
            } finally {
                document.getElementById('loadershit').style.display = 'none';
            }
        }
    };
</script>

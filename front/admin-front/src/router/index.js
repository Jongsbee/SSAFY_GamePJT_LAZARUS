import Vue from "vue";
import VueRouter from "vue-router";
import HomeView from "@/views/HomeView.vue";
import SearchResultView from "@/views/SearchResult.vue";

Vue.use(VueRouter);

const routes = [
    {
        path: "/",
        name: "home",
        component: HomeView,
    },
    {
        path: "/search",
        name: "SearchResult",
        component: SearchResultView,
    },
];

const router = new VueRouter({
    routes,
});

export default router;

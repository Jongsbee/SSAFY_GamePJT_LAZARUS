import Vue from "vue";
import VueRouter from "vue-router";
import HomeView from "@/views/HomeView.vue";
import SearchResultView from "@/views/SearchResult.vue";
import Statistics from "@/views/StatisticsView.vue";
import Ranking from "@/views/RankingView.vue";
import Patch from "@/views/PatchNote.vue";

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
    {
        path: "/statistics",
        name: "statistics",
        component: Statistics,
    },
    {
        path: "/ranking",
        name: "ranking",
        component: Ranking,
    },
    {
        path: "/patch",
        name: "patch",
        component: Patch,
    },
];

const router = new VueRouter({
    routes,
    //mode: "history",
});

export default router;

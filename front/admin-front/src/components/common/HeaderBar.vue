<template>
    <div class="header">
        <b-navbar style="background-color: #0b3b17" class="d-flex align-items-center">
            <b-navbar-nav>
                <b-navbar-brand href="#" class="bold_text text-white" @click="goHome"
                    >LAZARUS</b-navbar-brand
                >
                <b-nav-item
                    ><span class="text-white text-small mx-3" @click="goStatistics"
                        >통계</span
                    ></b-nav-item
                >
                <b-nav-item
                    ><span class="text-white text-small mx-3" @click="goRank"
                        >랭킹</span
                    ></b-nav-item
                >
                <b-nav-item
                    ><span class="text-white text-small mx-3" @click="goPatch"
                        >패치노트</span
                    ></b-nav-item
                >
            </b-navbar-nav>
        </b-navbar>
    </div>
</template>

<script>
export default {
    name: "HeaderBar",
    methods: {
        goHome() {
            if (this.$route.path !== "/") {
                this.$router.push({ name: "home" });
            }
        },
        goRank() {
            if (this.$route.path !== "/ranking") {
                this.$router.push({ name: "ranking" });
            }
        },
        goStatistics() {
            if (this.$route.path !== "/statistics") {
                this.$router.push({ name: "statistics" });
            }
        },
        goPatch() {
            if (this.$route.path !== "/patch") {
                this.$router.push({ name: "patch" });
            }
        },
    },
};
</script>

<style scoped>
.bold_text {
    font-weight: 800;
}
.text-white {
    font-weight: 700;
    color: white;
}
.text-small {
    font-size: 1em;
}
</style>

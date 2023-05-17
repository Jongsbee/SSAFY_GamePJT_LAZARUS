<template>
    <div class="header">
        <b-navbar style="background-color: #0b3b17">
            <b-navbar-nav>
                <b-navbar-brand href="#" class="bold_text text-white" @click="goHome"
                    >LAZARUS</b-navbar-brand
                >
                <b-nav-item
                    ><span class="text-white mx-3" @click="goStatistics">통계</span></b-nav-item
                >
                <b-nav-item><span class="text-white" @click="goRank">랭킹</span></b-nav-item>
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
    },
};
</script>

<style scoped>
.bold_text {
    font-weight: 800px;
}
.text-white {
    font-weight: 500;
    color: white;
}
</style>

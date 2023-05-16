<template>
    <div class="header">
        <b-navbar type="dark" variant="dark" style="background-color: #464b4c">
            <b-navbar-nav>
                <b-navbar-brand href="#" class="bold_text" @click="goHome">MSA</b-navbar-brand>
                <b-nav-item><span class="text-white mx-3">통계</span></b-nav-item>
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
    },
};
</script>

<style scoped>
.bold_text {
    font-weight: bold;
}
.text-white {
    font-weight: 500;
    color: white;
}
</style>

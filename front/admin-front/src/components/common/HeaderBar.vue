<template>
    <div class="header">
        <b-navbar type="dark" variant="dark">
            <b-navbar-nav>
                <b-navbar-brand href="#" class="bold_text" @click="goHome">MSA</b-navbar-brand>
                <b-nav-item><span class="text-white mx-3">통계</span></b-nav-item>
                <b-nav-item><span class="text-white">랭킹</span></b-nav-item>
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
                location.reload();
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

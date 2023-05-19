<template>
    <div>
        <HeaderBar />
        <router-view></router-view>
    </div>
</template>

<script>
import HeaderBar from "@/components/common/HeaderBar.vue";
//import FooterBar from "@/components/common/FooterBar.vue";

export default {
    name: "App",
    components: {
        HeaderBar,
        //FooterBar,
        // 추가 컴포넌트들
    },
};
</script>
<style></style>

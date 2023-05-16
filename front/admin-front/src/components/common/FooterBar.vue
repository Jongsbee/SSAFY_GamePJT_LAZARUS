<template>
    <div class="d-flex flex-column align-items-center justify-content-center">
        <footer class="footer" :class="{ relative: isFooterRelative }">
            <p>
                &copy; EXODIA | <span style="color: #d8d8d8">이용약관</span> |
                <span style="color: #d8d8d8">정보처리방침</span> |
                <span style="color: #ffbf00"> email : admin@exodia.kr</span>
            </p>
        </footer>
    </div>
</template>

<script>
export default {
    name: "FooterBar",
    data() {
        return {
            isFooterRelative: false,
        };
    },
    mounted() {
        this.handleScroll(); // 초기 위치 설정
        window.addEventListener("scroll", this.handleScroll);
        this.$router.afterEach(this.handleScrollOnRouteChange);
    },
    beforeUnmount() {
        window.removeEventListener("scroll", this.handleScroll);
        this.$router.afterEach(null); // afterEach 훅 제거
    },
    destroyed() {
        window.removeEventListener("scroll", this.handleScroll);
    },
    methods: {
        handleScroll() {
            if (window.innerHeight < document.body.offsetHeight) {
                this.isFooterRelative = true;
            } else {
                this.isFooterRelative = false;
            }
        },
        handleScrollOnRouteChange() {
            // 페이지 이동 후 스크롤 위치 재조정
            this.$nextTick(() => {
                this.handleScroll();
            });
        },
    },
};
</script>

<style scoped>
.footer {
    position: fixed;
    bottom: -10px;
    width: 100%;
    height: 50px;
    background-color: #343a40;
}

.relative {
    position: relative;
}
</style>

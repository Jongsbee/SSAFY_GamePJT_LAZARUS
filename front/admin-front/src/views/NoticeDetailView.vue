<template>
    <div class="d-flex flex-column align-items-center ranking_back">
        <div class="main d-flex flex-column align-items-center justify-content-start">
            <b-container
                class="d-flex flex-column align-items-center justify-content-center container_style"
            >
                <b-row class="row_size">
                    <b-col
                        class="d-flex flex-column col_shape align-items-start justify-content-start"
                    >
                        <div class="ranking_title d-flex align-items-end">
                            <div class="mr-auto">
                                <span class="ranking_title_font"> 업데이트 </span>
                            </div>
                        </div>
                        <div class="ranking_body d-flex align-items-center">
                            <div
                                class="mr-5 notice-type d-flex align-items-center justify-content-center"
                            >
                                <span class="notice-type-font">{{ notice.noticeType }} </span>
                            </div>
                            <div class="mr-auto d-flex align-items-center justify-content-center">
                                <span class="notice-type-font"> {{ notice.noticeTitle }} </span>
                            </div>
                            <div
                                class="ml-auto mr-3 d-flex align-items-center justify-content-center"
                            >
                                <span> {{ notice.noticeDate }} </span>
                            </div>
                        </div>
                        <div class="mt-5 detail-content" style="white-space: pre-line">
                            {{ notice.noticeContent }}
                        </div>
                    </b-col>
                </b-row>
            </b-container>
        </div>
    </div>
</template>

<script>
import axios from "axios";

export default {
    data() {
        return {
            notice: null,
        };
    },
    mounted() {
        let noticeId = this.$route.query.no;

        axios
            .get(process.env.VUE_APP_SERVER_URL + "/search/notice/detail/" + noticeId)
            .then((res) => {
                console.log(res.data);
                this.notice = res.data;
            })
            .catch(() => {
                console.log("페이지가 존재하지 않습니다");
                this.$router.push({ name: "patch" });
            });
    },
};
</script>

<style scoped>
.detail-content {
    font-size: 1.5em;
}
.notice-type {
    border-radius: 30px;
    border: 2px solid #e6e6e6;
    width: 7%;
}
.notice-type-font {
    font-size: 1.5em;
}
.main {
    width: 1600px;
    height: 100vh;
}
.title_font {
    font-size: 5em;
    /*
    background: linear-gradient(to bottom, #f6cece, #d358f7);
    -webkit-background-clip: text;
    color: transparent;
    */
    /*color: #04b486; */
    color: #0b3b17;
}
.row_size {
    width: 1200px;
    height: 700px;
    margin-bottom: 30px;
}
.container_style {
    margin-top: 50px;
}
.col_shape {
    border-radius: 60px;
    background-color: white;
    height: 100%;
}
.ranking_title {
    width: 100%;
    height: 10%;
    border-bottom: 5px solid #e6e6e6;
}
.ranking_body {
    width: 100%;
    height: 10%;
    border-bottom: 2px solid #e6e6e6;
}
.ranking_title_font {
    font-size: 3em;
    color: #0b3b17;
}
.time_font {
    color: #bdbdbd;
    font-size: 1.5em;
    margin-right: 15px;
}
</style>

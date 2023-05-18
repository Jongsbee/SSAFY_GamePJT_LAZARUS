<template>
    <div class="d-flex flex-column align-items-center ranking_back">
        <div class="main d-flex flex-column align-items-center justify-content-start">
            <div>
                <span class="title_font"
                    ><img
                        v-bind="mainProps"
                        src="../assets/LAZARUS_logo_horizontal.png"
                        alt="Responsive image"
                        style="display: block"
                /></span>
            </div>

            <b-container
                class="d-flex flex-column align-items-center justify-content-center container_style"
            >
                <b-row class="row_size">
                    <b-col class="d-flex col_shape align-items-start justify-content-start">
                        <div class="ranking_title d-flex align-items-center">
                            <div class="mr-auto">
                                <span class="ranking_title_font"> 시스템 권장사양 </span>
                            </div>
                            <div
                                class="ml-auto download-button d-flex align-items-center justify-content-center"
                            >
                                <div @click="openDownload">다운로드</div>
                            </div>
                        </div>
                    </b-col>
                </b-row>
            </b-container>
        </div>
    </div>
</template>

<script>
export default {
    data() {
        return {
            mainProps: {
                width: 720,
                height: 200,
                class: "my-5",
            },
        };
    },
    methods: {
        openDownload() {
            window.open(
                "https://drive.google.com/file/d/1oTWP19Do0pv8AlTNmD99BYWQ2X_MycDh/view",
                "_blank"
            );
        },
    },
};
</script>

<style scoped>
.ranking_back {
    /* background: linear-gradient(to bottom, #fbfbef, #2efec8); */

    height: 1000px;
}
.main {
    width: 1600px;
    height: 100vh;
}
.title_font {
    font-size: 5em;
    font-weight: bold;
    color: #0b3b17;
}
.row_size {
    width: 1000px;
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
    padding-bottom: 15px;
}
.ranking_body {
    width: 100%;
    height: 20%;
}
.ranking_body_2 {
    width: 100%;
    height: 5%;
}
.ranking_body_font {
    font-size: 1.5em;
    font-weight: bold;
    margin-left: 15px;
}
.ranking_title_font {
    font-size: 2em;
    color: #0b3b17;
}
.time_font {
    color: #bdbdbd;
    font-size: 1.5em;
    font-weight: bold;
    margin-right: 15px;
}
.download-button {
    background-color: #088a4b;
    color: white;
    font-size: 1.5em;
    height: 60px;
    width: 150px;
    padding: 3px;
    border: 5px solid #bcf5a9;
    border-radius: 30px;
    cursor: pointer;
}
</style>

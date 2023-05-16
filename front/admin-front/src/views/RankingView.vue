<template>
    <div class="d-flex flex-column align-items-center justify-content-center ranking_back">
        <div class="main d-flex flex-column align-items-center justify-content-center">
            <div>
                <span class="title_font"> MSA RANKING</span>
            </div>

            <b-container
                class="d-flex flex-column align-items-center justify-content-center container_style"
            >
                <b-row class="row_size">
                    <b-col class="d-flex flex-column col_shape mx-3">
                        <div class="ranking_title d-flex align-items-center justify-content-center">
                            <span class="ranking_title_font"> 최단기간 탈출 </span>
                        </div>
                        <span class="title_font"> 1 구역</span>
                    </b-col>
                    <b-col class="d-flex align-items-center justify-content-center col_shape mx-3">
                        <span class="title_font"> 2 구역</span>
                    </b-col>
                    <b-col class="d-flex align-items-center justify-content-center col_shape mx-3">
                        <span class="title_font"> 3 구역</span>
                    </b-col>
                    <b-col class="d-flex align-items-center justify-content-center col_shape mx-3">
                        <span class="title_font"> 4 구역</span>
                    </b-col>
                </b-row>
            </b-container>
        </div>
    </div>
</template>

<script>
export default {};
</script>

<style scoped>
.ranking_back {
    background: linear-gradient(to right bottom, #a2d5f2, #fcb8d2);
}
.main {
    width: 1600px;
    height: 100vh;
}
.title_font {
    font-size: 5em;
    font-weight: bold;
    background: linear-gradient(to bottom, #f6cece, #d358f7); /* 배경 그라데이션 설정 */
    -webkit-background-clip: text; /* 배경을 텍스트 영역으로 제한 */
    color: transparent; /* 텍스트 색상 투명 설정 */
}
.row_size {
    width: 1600px;
}
.container_style {
    margin-top: 50px;
}
.col_shape {
    border-radius: 60px;
    background-color: white;
    height: 600px;
    margin-bottom: 50px;
}
.ranking_title {
    width: 100%;
    height: 13%;
    border-bottom: 3px solid #e6e6e6;
}
.ranking_title_font {
    font-size: 2em;
    font-weight: bold;
    color: #a9d0f5;
}
</style>

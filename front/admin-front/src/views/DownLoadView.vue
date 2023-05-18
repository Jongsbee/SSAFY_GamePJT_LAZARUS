<template>
    <div class="d-flex flex-column align-items-center ranking_back">
        <div class="main d-flex flex-column align-items-center justify-content-start">
            <div>
                <img
                        v-bind="mainProps"
                        src="../assets/LAZARUS_logo_horizontal.png"
                        alt="Responsive image"
                        style="display: block"
                />
            </div>

            <b-container
                class="d-flex flex-column align-items-center justify-content-center container_style"
            >
                <b-row class="row_size">
                    <b-col class="d-flex flex-column col_shape align-items-start justify-content-start">
                        <div class="ranking_title d-flex align-items-center">
                            <div class="mr-auto">
                                <span class="ranking_title_font"> 시스템 권장사양 </span>
                            </div>
                            <div
                                class="ml-auto download-button d-flex align-items-center justify-content-center"
                            >
                                <div @click="openDownload">
                                    <img
                                        v-bind="subProps"
                                        src="../assets/white_download.png"
                                        alt="Responsive image"
                                    />다운로드
                                </div>
                            </div>
                        </div>
                        <table class="table-size mt-4">
                            <th class="d-flex align-items-center justify-content-center th-style">
                                <td class="th-column d-flex align-items-center justify-content-center th-br"> 권장 시스템 사양 </td>
                                <td class="th-column d-flex align-items-center justify-content-center th-br"> 최소 사양 </td>
                                <td class="th-column d-flex align-items-center justify-content-center"> 권장 사양</td>
                            </th>
                            <tbody class="d-flex align-items-center justify-content-center tbody-style">
                                <td class="th-column d-flex align-items-center justify-content-center tbody-column"> PC , Laptop</td>
                                <td class="th-column d-flex align-items-center justify-content-center tbody-column"> 싸트북</td>
                                <td class="th-column d-flex align-items-center justify-content-center tbody-top"> 싸트북 보다 좋은거</td>
                            </tbody>
                            <tbody class="d-flex align-items-center justify-content-center tbody-style tbody-bottom">
                                <td class="th-column d-flex align-items-center justify-content-center tbody-column"> OS </td>
                                <td class="th-column d-flex align-items-center justify-content-center tbody-column"> Window </td>
                                <td class="th-column d-flex align-items-center justify-content-center tbody-top"> SSAFY 에서 Mac..? </td>
                            </tbody>
                        </table>
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
            subProps: {
                width: 18,
                height: 18,
                class: "mr-2 mb-1",
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
.tbody-bottom{
    border-bottom: 2px solid #58FA82;
}
.tbody-style{
    background-color : white;
}
.tbody-column{
    border-right: 2px solid #58FA82;
    border-top: 2px solid #58FA82;
}
.tbody-top{
    border-top: 2px solid #58FA82; 
}

.th-style{
    background-color : #E0F8E6; 
}
.th-br{
    border-right: 2px solid #58FA82;
}
.th-column{
    width: 33%;
    font-size: 2em;
    color : #0b3b17;
}
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
    width: 160px;
    padding: 3px;
    border: 5px solid #bcf5a9;
    border-radius: 30px;
    cursor: pointer;
}
.table-size{
    width: 100%;
}
</style>

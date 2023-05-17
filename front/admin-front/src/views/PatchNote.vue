<template>
    <div class="d-flex flex-column align-items-center ranking_back">
        <div class="main d-flex flex-column align-items-center justify-content-start">
            <div>
                <span class="title_font">📗PatchNote📗</span>
            </div>

            <b-container
                class="d-flex flex-column align-items-center justify-content-center container_style"
            >
                <b-row class="row_size">
                    <b-col class="d-flex flex-column col_shape mx-3"> </b-col>
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
    background: linear-gradient(to bottom, #fbfbef, #2efec8);
    height: 1000px;
}
.main {
    width: 1600px;
    height: 100vh;
}
.title_font {
    font-size: 5em;
    font-weight: bold;
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
    height: 20%;
    border-bottom: 5px solid #e6e6e6;
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
    font-weight: bold;
    color: #04b486;
}
.time_font {
    color: #bdbdbd;
    font-size: 1.5em;
    font-weight: bold;
    margin-right: 15px;
}
</style>

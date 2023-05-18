<template>
    <div class="container d-flex flex-column align-items-center main">
        <div class="row justify-content-center">
            <div>
                <img
                    v-bind="mainProps"
                    src="../assets/LAZARUS_logo_horizontal.png"
                    alt="Responsive image"
                    style="display: block"
                    class="mr-3"
                />
            </div>

            <!-- <div class="title-font my-5 text-center">LAZARUS.GG</div> -->

            <div class="col-md-12">
                <b-input-group variant="outline-primary">
                    <b-input-group-prepend>
                        <b-input-group-text class="table_header">닉네임</b-input-group-text>
                    </b-input-group-prepend>
                    <b-form-input
                        v-model="nickname"
                        @keyup.enter="search"
                        style="height: 50px"
                        class="table_header"
                    ></b-form-input>
                    <b-input-group-append class="input_append">
                        <b-input-group-text class="table_header clickable" @click="search"
                            >검색</b-input-group-text
                        >
                    </b-input-group-append>
                </b-input-group>
            </div>
        </div>
    </div>
</template>

<script>
export default {
    data() {
        return {
            nickname: "",
            mainProps: {
                width: 720,
                height: 200,
                class: "my-5",
            },
        };
    },
    methods: {
        search() {
            if (this.nickname === "") {
                alert("닉네임을 입력해주세요");
                return;
            }
            let param = this.nickname.replace(/\s/g, "");
            this.$router.push({ name: "SearchResult", query: { q: param } });
        },
    },
};
</script>

<style scoped>
.sub {
    width: 1200px;
}
.title-font {
    font-size: 10em;
    color: #0b3b17;
}
.table_header {
    font-size: 1.3em;
}
.bold_text {
    background-color: #ffffff;
}

.search {
    border-radius: 5px;
}

.container {
    display: flex;
    height: 100%;
}

.row {
    width: 100%; /* 화면 전체 너비를 채우기 위해 row에 너비 설정 */
}

.col-md-12 {
    text-align: center; /* 요소를 수평 가운데 정렬하기 위해 col-md-6에 텍스트 정렬 설정 */
}
.clickable {
    cursor: pointer;
}
.bottom-left {
    position: relative; /* 절대적 위치 설정 */
    bottom: 0; /* 하단에 배치 */
    left: 10px; /* 왼쪽에 배치 */
}
</style>

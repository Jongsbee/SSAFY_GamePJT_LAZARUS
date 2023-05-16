<template>
    <div class="container d-flex flex-column">
        <div class="row justify-content-center">
            <img
                v-bind="mainProps"
                src="../assets/home-image.jpg"
                alt="Responsive image"
                style="display: block"
            />
            <div class="col-md-12">
                <b-input-group variant="outline-primary">
                    <b-input-group-prepend>
                        <b-input-group-text class="bold_text">닉네임</b-input-group-text>
                    </b-input-group-prepend>
                    <b-form-input v-model="nickname" @keyup.enter="search"></b-form-input>
                    <b-input-group-append class="input_append">
                        <b-input-group-text class="bold_text clickable" @click="search"
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
                width: 800,
                height: 300,
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
.bold_text {
    font-weight: bold;
    background-color: #ffffff;
}

.search {
    border-radius: 5px;
}

.container {
    display: flex;
    height: 100vh;
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

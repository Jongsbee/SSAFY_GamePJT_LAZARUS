<template>
    <div class="d-flex flex-column align-items-center justify-content-center">
        <div class="main mt-3">
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
        <div
            class="d-flex flex-column align-items-center mx-auto main px-3 py-3 mt-3"
            v-if="player.normalKill !== null"
        >
            <b-container class="player_info my-5">
                <b-row>
                    <b-col cols="2" class="d-flex justify-content-center">
                        <img src="../assets/yang.jpg" alt="user" width="100" height="100" />
                    </b-col>
                    <b-col class="d-flex align-items-center" cols="7">
                        <div>
                            <h1 class="text_bold">{{ searchParam }}</h1>
                        </div>
                    </b-col>

                    <b-col
                        cols="3"
                        class="d-flex flex-column align-items-center justify-content-center"
                    >
                        <div class="text_bold">
                            총 플레이 시간 <br />
                            <span style="color: #d358f7">{{ player.playTime }}</span>
                        </div>
                    </b-col>
                </b-row>
                <b-row class="mt-5 mb-3 d-flex align-items-center align-items-stretch">
                    <b-col cols="3" class="bg_white">
                        <div class="text_bold">
                            탈출율
                            <span class="text_big">{{ player.escapePercentage }}%</span>
                        </div>
                        <div class="text_bold">
                            <span class="text_sbig">{{ player.totalGame }}</span> 전
                            <span class="text_sbig" style="color: #81f781">{{
                                player.doEscape
                            }}</span>
                            <span style="color: #81f781">탈출</span>
                            <span class="text_sbig" style="color: #f5a9a9"> {{ player.die }}</span>
                            <span style="color: #f5a9a9">사망</span>
                        </div>
                    </b-col>
                    <b-col
                        cols="3"
                        class="bg_white d-flex flex-column align-items-center justify-content-center"
                    >
                        <div class="text_bold">
                            처치힌 일반 몬스터 &nbsp;
                            <span style="color: #bdbdbd">{{ player.normalKill }}</span>
                        </div>
                        <div class="text_bold">
                            처치한 엘리트 몬스터 &nbsp;
                            <span style="color: #bdbdbd">{{ player.eliteKill }}</span>
                        </div>
                    </b-col>
                    <b-col
                        cols="3"
                        class="bg_white d-flex flex-column align-items-center justify-content-center"
                    >
                        <div class="text_bold">
                            제작한 아이템 수 &nbsp;
                            <span style="color: #bdbdbd">{{ player.item }}</span>
                        </div>
                        <div class="text_bold">
                            클리어한 퀘스트 수 &nbsp;
                            <span style="color: #bdbdbd">{{ player.quest }}</span>
                        </div>
                    </b-col>
                    <b-col
                        class="bg_white d-flex flex-column align-items-center justify-content-center"
                    >
                        <div class="text_bold">
                            최단 탈출 기록 <br />
                            <span style="color: #bdbdbd">{{ player.shortestEscape }}</span> <br />
                            최장 생존 기록 <br />
                            <span style="color: #bdbdbd">{{ player.longestSurvival }}</span>
                        </div>
                    </b-col>
                </b-row>
            </b-container>
            <table class="table">
                <thead>
                    <tr>
                        <th>탈출여부</th>
                        <th>일반 몬스터 처치</th>
                        <th>엘리트 몬스터 처치</th>
                        <th>아이템 조합</th>
                        <th>클리어 퀘스트 수</th>
                        <th>게임 진행 시간</th>
                        <th>게임 진행 날짜</th>
                    </tr>
                </thead>
                <tbody>
                    <tr
                        v-for="(game, index) in games"
                        :key="index"
                        :style="{
                            'background-color': game.result === '탈출' ? '#CEE3F6' : '#F5A9A9',
                        }"
                    >
                        <td class="text_bold">{{ game.result }}</td>
                        <td class="text_bold">{{ game.normal }}</td>
                        <td class="text_bold">{{ game.elite }}</td>
                        <td class="text_bold">{{ game.item }}</td>
                        <td class="text_bold">{{ game.quest }}</td>
                        <td class="text_bold">{{ game.gameTime }}</td>
                        <td class="text_bold">{{ game.when }} 전</td>
                    </tr>
                </tbody>
            </table>
            <b-button
                @click="findUserRecord"
                block
                variant="secondary"
                v-if="games.length % 10 === 0 && games.length !== 0"
                class="mt-3 button_size"
                >전적 더보기</b-button
            >
        </div>
        <div v-if="player.normalKill === null" class="mt-5 width_full">
            <span class="text_big" style="color: #bdbdbd">사용자 정보가 존재하지 않습니다.</span>
        </div>
    </div>
</template>

<script>
import axios from "axios";

export default {
    data() {
        return {
            nickname: "",
            isSearch: true,
            searchParam: "",
            pageNum: 0,
            player: {
                normalKill: null,
                eliteKill: null,
                escapePercentage: null,
                totalGame: null,
                doEscape: null,
                die: null,
                quest: null,
                item: null,
                playTime: null,
                longestSurvival: null,
                shortestEscape: null,
            },
            games: [],
        };
    },
    created() {
        this.games = [];
        this.pageNum = 0;
        this.findUserInfo();
    },
    watch: {
        isSearch: function () {
            this.games = [];
            this.pageNum = 0;
            this.findUserInfo();
        },
    },
    methods: {
        findUserRecord() {
            let pageCnt = this.pageNum;
            console.log(pageCnt);
            axios
                .get(process.env.VUE_APP_SERVER_URL + "/search/record", {
                    params: {
                        nickname: this.searchParam, // 쿼리 파라미터로 보낼 값
                        page: pageCnt,
                    },
                })
                .then((response) => {
                    // 요청 성공 시 처리할 로직
                    console.log(response.data);
                    this.pageNum++;
                    for (let index = 0; index < response.data.length; index++) {
                        const element = response.data[index];
                        this.games.push(element);
                    }
                })
                .catch(() => {});
        },
        findUserInfo() {
            console.log(process.env.VUE_APP_SERVER_URL);
            this.searchParam = this.$route.query.q;
            console.log(this.searchParam);
            if (this.searchParam === undefined) {
                alert("잘못된 접근입니다");
                this.$router.push({ name: "home" });
            }

            axios
                .get(process.env.VUE_APP_SERVER_URL + "/search/user", {
                    params: {
                        nickname: this.searchParam, // 쿼리 파라미터로 보낼 값
                    },
                })
                .then((response) => {
                    // 요청 성공 시 처리할 로직
                    let userData = response.data;

                    this.player.normalKill = userData.normalMonsterKills;
                    this.player.eliteKill = userData.eliteMonsterKills;
                    this.player.doEscape = userData.totalEscapeCount;
                    this.player.die = userData.deathCount;
                    this.player.quest = userData.totalQuestCompleted;
                    this.player.item = userData.totalItemCrafted;
                    this.player.totalGame = this.player.die + this.player.doEscape;
                    this.player.escapePercentage = (
                        (this.player.doEscape / this.player.totalGame) *
                        100
                    ).toFixed(0);
                    this.getShortestTime(userData.shortestEscapeTime);
                    this.getLogestTime(userData.longestSurvivalTime);
                    this.getTotalPlayTime(userData.totalPlayTime);
                })
                .catch(() => {
                    alert("존재하지 않는 사용자입니다.");
                    //this.$router.push({ name: "home" });
                });
            this.findUserRecord();
        },
        getShortestTime(seconds) {
            const hours = Math.floor(seconds / 3600);
            const minutes = Math.floor((seconds % 3600) / 60);
            const remainingSeconds = seconds % 60;

            var shortTime = "";
            if (hours === 0) {
                shortTime = minutes + "분" + remainingSeconds + "초";
            } else {
                shortTime = hours + "시간" + minutes + "분" + remainingSeconds + "초";
            }
            this.player.shortestEscape = shortTime;
        },
        getLogestTime(seconds) {
            const hours = Math.floor(seconds / 3600);
            const minutes = Math.floor((seconds % 3600) / 60);
            const remainingSeconds = seconds % 60;

            var longTime = "";
            if (hours === 0) {
                longTime = minutes + "분" + remainingSeconds + "초";
            } else {
                longTime = hours + "시간" + minutes + "분" + remainingSeconds + "초";
            }
            this.player.longestSurvival = longTime;
        },
        getTotalPlayTime(seconds) {
            const hours = Math.floor(seconds / 3600);
            const minutes = Math.floor((seconds % 3600) / 60);
            const remainingSeconds = seconds % 60;

            var totalTime = hours + "시간" + minutes + "분" + remainingSeconds + "초";
            this.player.playTime = totalTime;
        },
        search() {
            if (this.nickname === "") {
                alert("닉네임을 입력해주세요");
                return;
            }
            if (this.nickname === this.searchParam) {
                return;
            }
            this.player = {
                normalKill: null,
                eliteKill: null,
                escapePercentage: null,
                totalGame: null,
                doEscape: null,
                die: null,
                quest: null,
                item: null,
                playTime: null,
                longestSurvival: null,
                shortestEscape: null,
            };

            let param = this.nickname.replace(/\s/g, "");
            this.$router.push({ name: "SearchResult", query: { q: param } });
            if (this.isSearch) {
                this.isSearch = false;
            } else {
                this.isSearch = true;
            }
        },
    },
};
</script>

<style scoped>
.table {
    margin: 0 auto;
    width: 1150px;
}
.text_bold {
    font-weight: bold;
}
.main {
    width: 1400px; /* 화면 너비의 10% */
    background-color: #f2f2f2;
}
.text_big {
    font-size: 3em;
    font-weight: bold;
}
.text_sbig {
    font-size: 2em;
    font-weight: bold;
}
.bg_white {
    background-color: white;
    border: 1px solid #ccc;
}
.button_size {
    width: 1150px;
}
</style>

<template>
    <div class="d-flex flex-column align-items-center justify-content-center ranking_back">
        <div class="main d-flex flex-column align-items-center justify-content-start">
            <div>
                <span class="title_font">🏆RANKING🏆</span>
            </div>

            <b-container
                class="d-flex flex-column align-items-center justify-content-center container_style"
            >
                <b-row class="row_size">
                    <b-col class="d-flex flex-column col_shape mx-3">
                        <div class="ranking_title d-flex align-items-center justify-content-center">
                            <span class="ranking_title_font"> 최단기간 탈출 </span>
                        </div>
                        <div class="ranking_body_2"></div>
                        <div class="ranking_body d-flex align-items-center">
                            <div clas="mr-auto">
                                <span class="ranking_body_font">
                                    🥇 {{ timeRank[0].nickname }}
                                </span>
                            </div>
                            <div class="ml-auto">
                                <span class="time_font">{{ timeRank[0].time }}</span>
                            </div>
                        </div>
                        <div class="ranking_body d-flex align-items-center">
                            <div clas="mr-auto">
                                <span class="ranking_body_font">
                                    🥈 {{ timeRank[1].nickname }}
                                </span>
                            </div>
                            <div class="ml-auto">
                                <span class="time_font">{{ timeRank[1].time }}</span>
                            </div>
                        </div>
                        <div class="ranking_body d-flex align-items-center">
                            <div clas="mr-auto">
                                <span class="ranking_body_font">
                                    🥉 {{ timeRank[2].nickname }}
                                </span>
                            </div>
                            <div class="ml-auto">
                                <span class="time_font">{{ timeRank[2].time }}</span>
                            </div>
                        </div>
                    </b-col>
                    <b-col class="d-flex flex-column col_shape mx-3">
                        <div class="ranking_title d-flex align-items-center justify-content-center">
                            <span class="ranking_title_font"> 최다 몬스터 토벌 </span>
                        </div>
                        <div class="ranking_body_2"></div>
                        <div class="ranking_body d-flex align-items-center">
                            <div clas="mr-auto">
                                <span class="ranking_body_font">
                                    🥇 {{ monsterKill[0].nickname }}
                                </span>
                            </div>
                            <div class="ml-auto">
                                <span class="time_font">{{ monsterKill[0].cnt }} 회</span>
                            </div>
                        </div>
                        <div class="ranking_body d-flex align-items-center">
                            <div clas="mr-auto">
                                <span class="ranking_body_font">
                                    🥈 {{ monsterKill[1].nickname }}
                                </span>
                            </div>
                            <div class="ml-auto">
                                <span class="time_font">{{ monsterKill[1].cnt }} 회</span>
                            </div>
                        </div>
                        <div class="ranking_body d-flex align-items-center">
                            <div clas="mr-auto">
                                <span class="ranking_body_font">
                                    🥉 {{ monsterKill[2].nickname }}
                                </span>
                            </div>
                            <div class="ml-auto">
                                <span class="time_font">{{ monsterKill[2].cnt }} 회</span>
                            </div>
                        </div>
                    </b-col>
                </b-row>
                <b-row class="row_size">
                    <b-col class="d-flex flex-column col_shape mx-3">
                        <div class="ranking_title d-flex align-items-center justify-content-center">
                            <span class="ranking_title_font"> 최다 아이템 제작 </span>
                        </div>
                        <div class="ranking_body_2"></div>
                        <div class="ranking_body d-flex align-items-center">
                            <div clas="mr-auto">
                                <span class="ranking_body_font">
                                    🥇 {{ itemCraft[0].nickname }}
                                </span>
                            </div>
                            <div class="ml-auto">
                                <span class="time_font">{{ itemCraft[0].cnt }} 회</span>
                            </div>
                        </div>
                        <div class="ranking_body d-flex align-items-center">
                            <div clas="mr-auto">
                                <span class="ranking_body_font">
                                    🥈 {{ itemCraft[1].nickname }}
                                </span>
                            </div>
                            <div class="ml-auto">
                                <span class="time_font">{{ itemCraft[1].cnt }} 회</span>
                            </div>
                        </div>
                        <div class="ranking_body d-flex align-items-center">
                            <div clas="mr-auto">
                                <span class="ranking_body_font">
                                    🥉 {{ itemCraft[2].nickname }}
                                </span>
                            </div>
                            <div class="ml-auto">
                                <span class="time_font">{{ itemCraft[2].cnt }} 회</span>
                            </div>
                        </div>
                    </b-col>
                    <b-col class="d-flex flex-column col_shape mx-3">
                        <div class="ranking_title d-flex align-items-center justify-content-center">
                            <span class="ranking_title_font"> 최다 퀘스트 클리어 </span>
                        </div>
                        <div class="ranking_body_2"></div>
                        <div class="ranking_body d-flex align-items-center">
                            <div clas="mr-auto">
                                <span class="ranking_body_font">
                                    🥇 {{ quetClear[0].nickname }}
                                </span>
                            </div>
                            <div class="ml-auto">
                                <span class="time_font">{{ quetClear[0].cnt }} 회</span>
                            </div>
                        </div>
                        <div class="ranking_body d-flex align-items-center">
                            <div clas="mr-auto">
                                <span class="ranking_body_font">
                                    🥈 {{ quetClear[1].nickname }}
                                </span>
                            </div>
                            <div class="ml-auto">
                                <span class="time_font">{{ quetClear[1].cnt }} 회</span>
                            </div>
                        </div>
                        <div class="ranking_body d-flex align-items-center">
                            <div clas="mr-auto">
                                <span class="ranking_body_font">
                                    🥉 {{ quetClear[2].nickname }}
                                </span>
                            </div>
                            <div class="ml-auto">
                                <span class="time_font">{{ quetClear[2].cnt }} 회</span>
                            </div>
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
            timeRank: [],
            monsterKill: [],
            itemCraft: [],
            quetClear: [],
        };
    },
    created() {
        this.findRanking();
    },
    methods: {
        findRanking() {
            axios
                .get(process.env.VUE_APP_SERVER_URL + "/search/ranking")
                .then((res) => {
                    console.log(res.data);
                    this.timeRank = res.data.escapeRanks;
                    this.monsterKill = res.data.huntRanks;
                    this.itemCraft = res.data.craftRanks;
                    this.quetClear = res.data.questRanks;
                })
                .catch((error) => {
                    console.log(error);
                });
        },
    },
};
</script>

<style scoped>
.ranking_back {
    background: linear-gradient(to bottom, #fbfbef, #2efec8);
    height: 100%;
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
    height: 300px;
    margin-bottom: 30px;
}
.container_style {
    margin-top: 50px;
}
.col_shape {
    border-radius: 60px;
    background-color: white;
    height: 300px;
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

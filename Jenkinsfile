pipeline {
    agent any

    tools {
        nodejs "nodejs"
    }

    stages {
        stage('Project Build') {
            steps {
                script {
                    if(env.BRANCH_NAME == 'feature-front/admin') {
                        echo "Front Project Build Step"
                        dir('front/admin-front') {
                            withCredentials([file(credentialsId: 'env-file', variable: 'ENV_FILE')]) {
                                sh 'cp $ENV_FILE .env'
                                sh 'npm install'
                                sh 'npm run build'
                            }
                        }
                    } else if(env.BRANCH_NAME == 'api-server/member') {
                        echo "Main Server Project Build Step"
                        dir('api-server/MainServer') {
                            sh 'chmod +x gradlew'
                            sh './gradlew clean build -x test'
                        }
                    } else if(env.BRANCH_NAME == 'api-server/logserver') {
                        echo "Log Server Project Build Step"
                        dir('api-server/LogServer') {
                            sh 'chmod +x gradlew'
                            sh './gradlew clean build -x test'
                        }
                    } else if(env.BRANCH_NAME == 'scheduler-server/schedulerServer') {
                        echo "Scheduler Server Project Build Step"
                        dir('scheduler-server/SchedulerServer') {
                            sh 'chmod +x gradlew'
                            sh './gradlew clean build -x test'
                        }
                    }
                }
            }
        }
        stage('Image Build') {
            environment {
                PATH = "/busybox:/kaniko:$PATH"
            }
            steps {
                script {
                    podTemplate(yaml: """
                      kind: Pod
                      metadata:
                        name: kaniko
                      spec:
                        containers:
                        - name: kaniko
                          image: gcr.io/kaniko-project/executor:debug
                          imagePullPolicy: Always
                          command:
                          - sleep
                          args:
                          - 99d
                          volumeMounts:
                          - name: shared-workspace
                            mountPath: /workspace
                          - name: docker-config
                            mountPath: /kaniko/.docker
                          tty: true
                        nodeSelector:
                          node-role.kubernetes.io/control-plane: ""
                        volumes:
                        - name: shared-workspace
                          hostPath:
                            path: ${WORKSPACE}
                            type: Directory
                        - name: docker-config
                          secret:
                            secretName: regcred
                            items:
                            - key: .dockerconfigjson
                              path: config.json
                        """) {
                        node(POD_LABEL) {
                            container(name: 'kaniko', shell: '/busybox/sh') {
                                if(env.BRANCH_NAME == 'feature-front/admin') {
                                    echo "Front Image Build Step"
                                    sh """#!/busybox/sh
                                    /kaniko/executor --context=/workspace/front/admin-front --dockerfile=/workspace/front/admin-front/Dockerfile --destination=sadoruin/msa-admin-front:${env.BUILD_NUMBER}
                                    """
                                } else if(env.BRANCH_NAME == 'api-server/member') {
                                    echo "Main Server Image Build Step"
                                    sh """#!/busybox/sh
                                    /kaniko/executor --context=/workspace/api-server/MainServer --dockerfile=/workspace/api-server/MainServer/Dockerfile --destination=sadoruin/msa-mainserver:${env.BUILD_NUMBER}
                                    """
                                } else if(env.BRANCH_NAME == 'api-server/logserver') {
                                    echo "Log Server Image Build Step"
                                    sh """#!/busybox/sh
                                    /kaniko/executor --context=/workspace/api-server/LogServer --dockerfile=/workspace/api-server/LogServer/Dockerfile --destination=sadoruin/msa-logserver:${env.BUILD_NUMBER}
                                    """
                                } else if(env.BRANCH_NAME == 'scheduler-server/schedulerServer') {
                                    echo "Scheduler Server Image Build Step"
                                    sh """#!/busybox/sh
                                    /kaniko/executor --context=/workspace/scheduler-server/SchedulerServer --dockerfile=/workspace/scheduler-server/SchedulerServer/Dockerfile --destination=sadoruin/msa-schedulerserver:${env.BUILD_NUMBER}
                                    """
                                }
                            }
                        }
                    }
                }
            }
        }
        stage('Deploy') {
            steps {
                script {
                    dir('/git') {
                        git branch: 'main',
                            credentialsId: 'gitlab-account',
                            url: 'https://lab.ssafy.com/dope2514/s08p31a106-gitops.git'
                        sh 'git config --global user.email "dope2514@gmail.com"'
                        sh 'git config --global user.name "SadoRuin"'

                        if(env.BRANCH_NAME == 'feature-front/admin') {
                            echo "Front Deploy Step"
                            sh """
                                sed -i 's/msa-admin-front:\\([^:]*\\)/msa-admin-front:${env.BUILD_NUMBER}/g' servers/admin-front.yaml
                                git add front/admin-front.yaml
                                git commit -m 'Update msa-admin-front tag to ${env.BUILD_NUMBER}'
                            """
                        } else if(env.BRANCH_NAME == 'api-server/member') {
                            echo "Main Server Deploy Step"
                            sh """
                                sed -i 's/msa-mainserver:\\([^:]*\\)/msa-mainserver:${env.BUILD_NUMBER}/g' servers/msa-mainserver.yaml
                                git add servers/msa-mainserver.yaml
                                git commit -m 'Update msa-mainserver tag to ${env.BUILD_NUMBER}'
                            """
                        } else if(env.BRANCH_NAME == 'api-server/logserver') {
                            echo "Log Server Deploy Step"
                            sh """
                                sed -i 's/msa-logserver:\\([^:]*\\)/msa-logserver:${env.BUILD_NUMBER}/g' servers/msa-logserver.yaml
                                git add servers/msa-logserver.yaml
                                git commit -m 'Update msa-logserver tag to ${env.BUILD_NUMBER}'
                            """
                        } else if(env.BRANCH_NAME == 'scheduler-server/schedulerServer') {
                            echo "Scheduler Server Deploy Step"
                            sh """
                                sed -i 's/msa-schedulerserver:\\([^:]*\\)/msa-schedulerserver:${env.BUILD_NUMBER}/g' servers/msa-schedulerserver.yaml
                                git add servers/msa-schedulerserver.yaml
                                git commit -m 'Update msa-schedulerserver tag to ${env.BUILD_NUMBER}'
                            """
                        }

                        withCredentials([usernamePassword(credentialsId: 'gitlab-account', passwordVariable: 'GIT_PASSWORD', usernameVariable: 'GIT_USERNAME')]) {
                            sh 'git remote set-url origin https://$GIT_USERNAME:$GIT_PASSWORD@lab.ssafy.com/dope2514/s08p31a106-gitops.git'
                            sh 'git push origin main'
                        }
                    }

                }
            }
        }
    }
}

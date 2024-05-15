<template>
    <div class="manage-musics">
        <div class="manage-musicsearch" style="margin-top: 20px;margin-left: 30px;">
            <el-input style="width: 300px;" v-model="KeyWord" placeholder="请输入内容" clearable> <el-button
                    @click="MusicSearch" slot="append" icon="el-icon-search">搜索</el-button></el-input>
            <el-button type="primary" style="margin-left: 20px;" @click="AlbumAdd">新增</el-button>
        </div>



        <div class="manage-musiclist" style="margin-top: 30px;margin-left: 100px;">
            <el-table :data="tableData" style="width: 90%;margin-left: 30px;">
                <el-table-column prop="id" label="主键" v-if="showIdColumn"> </el-table-column>
                <el-table-column prop="album_Name" label="专辑名称" width="600px"> </el-table-column>
                <el-table-column prop="album_Info" label="专辑信息"></el-table-column>
                <el-table-column prop="create_Time" label="创建时间"></el-table-column>
                <el-table-column label="操作">
                    <template slot-scope="scope">
                        <el-button size="mini" @click="editMusic(scope.row)">编辑</el-button>
                        <el-button size="mini" type="danger" @click="deleteMusic(scope.row)">删除</el-button>
                    </template>
                </el-table-column>
            </el-table>
        </div>


        <div>
            <el-dialog :visible.sync="dialogVisibleAdd" title="新增专辑" width="50%">
                <el-form>
                    <!-- 在这里添加表单字段，例如： -->
                    <el-form-item label="专辑名称">
                        <el-input v-model="Add.album_Name"></el-input>
                    </el-form-item>
                    <el-form-item label="专辑信息">
                        <el-input v-model="Add.album_Info"></el-input>
                    </el-form-item>

                    <el-form-item label="歌曲">
                        <el-select v-model="Add.SonsList" multiple placeholder="请选择">
                            <el-option v-for="item in SonsList" :key="item.id" :label="item.music_Name"
                                :value="item.id">
                            </el-option>
                        </el-select>
                    </el-form-item>

                </el-form>
                <div slot="footer" class="dialog-footer">
                    <el-button @click="dialogVisibleAdd = false">取 消</el-button>
                    <el-button type="primary" @click="submitAdd">确 定</el-button>
                </div>
            </el-dialog>
        </div>


        <div>
            <!-- 编辑对话框 -->
            <el-dialog :visible.sync="dialogVisibleEdite" title="编辑专辑" width="50%">
                <el-form :model="AddselectedRow">
                    <el-form-item label="专辑名称">
                        <el-input v-model="AddselectedRow.album_Name"></el-input>
                    </el-form-item>
                    <el-form-item label="专辑信息">
                        <el-input v-model="AddselectedRow.album_Info"></el-input>
                    </el-form-item>

                    <el-form-item label="歌曲">
                        <el-select v-model="AddselectedRow.sonsInfoList" multiple placeholder="请选择">
                            <el-option v-for="item in SonsList" :key="item.id" :label="item.music_Name"
                                :value="item.id">
                            </el-option>
                        </el-select>
                    </el-form-item>
                </el-form>
                <div slot="footer" class="dialog-footer">
                    <el-button @click="dialogVisibleEdite = false">取 消</el-button>
                    <el-button type="primary" @click="updateForm">确 定</el-button>
                </div>
            </el-dialog>
        </div>
    </div>
</template>

<script>

export default {
    data() {
        return {
            showIdColumn: false, // 初始隐藏id列
            dialogVisibleAdd: false, // 对话框是否可见
            dialogVisibleEdite:false,
            KeyWord: '',
            tableData: [],
            AddselectedRow: {
                id: '',
                album_Name: '',
                album_Info: '',
            },
            form: {
                id: '',
                album_Name: '',
                album_Info: '',
            },
            Add: {
                id: '',
                album_Name: '',
                album_Info: '',
                SonsList:[]
            },
            SonsList:[]
        }
    },
    created() {
        try {
            this.$http.get(`/Musics/GetMusicAlbumList?KeyWord=${this.KeyWord}`).then(res => {
                if (res.data.code === '200') {
                    this.tableData = res.data.data
                } else {
                    this.$message.error(res.data.msg)
                }
            })
        } catch (error) {
            this.$message.error('请求失败，请检查网络连接')
        }
    },
    methods: {
        MusicSearch() {
            try {
                this.$http.get(`/Musics/GetMusicAlbumList?KeyWord=${this.KeyWord}`).then(res => {
                    if (res.data.code === '200') {
                        this.tableData = res.data.data
                    } else {
                        this.$message.error(res.data.msg)
                    }
                })
            } catch (error) {
                this.$message.error('请求失败，请检查网络连接')
            }
        },
        submitAdd() {
            try {
                this.$http.post('/Musics/AddMusicAlbum', this.Add).then(res => {
                    if (res.data.code === '200') {
                        this.dialogVisibleAdd = false
                        this.$message.success('新增成功')
                        this.MusicSearch()
                    } else {
                        this.$message.error(res.data.messge)
                    }
                })
            } catch (error) {
                this.$message.error('请求失败，请检查网络连接')
            }
        },
        editMusic(row) {
            this.$http.get(`/Musics/GetMusicAlbum?Id=${row.id}`).then(res => {
                this.AddselectedRow = res.data.data;
                this.SonsList = res.data.data.sonsInfoList;
                setTimeout(() => {
                    this.dialogVisibleEdite = true;
                }, 100);
            })
            
        },
        updateForm() {
            try {
                this.$http.post('/Musics/EditeMusicAlbum', this.AddselectedRow).then(res => {
                    if (res.data.code === '200') {
                        this.dialogVisibleEdite = false
                        this.$message.success('修改成功')
                        this.MusicSearch()
                    } else {
                        this.$message.error(res.data.messge)
                    }
                })

            } catch (error) {
                this.$message.error('请求失败，请检查网络连接')
            }
        },
        deleteMusic(row) {
            this.$confirm('确定要删除吗?', '提示', {
                confirmButtonText: '确定',
                cancelButtonText: '取消',
                type: 'warning'
            }).then(() => {
                // 在这里添加删除请求的逻辑
                this.$http.get(`/Musics/DeleteMusicAlbum?Id=${row.id}`).then(res => {
                    if (res.data.code === '200') {
                        this.$message.success('删除成功')
                        this.MusicSearch()
                    }
                })
            }).catch(() => {
                // 用户取消删除操作
                console.log('删除操作已取消');
            });
        },
        AlbumAdd (){
            this.dialogVisibleAdd=true;
            this.$http.get('/Musics/GetMusicList').then(res => {
                this.SonsList = res.data.data
            })
        }
    }
}
</script>
<template>
    <div class="manage-musics">
        <div class="manage-musicsearch" style="margin-top: 20px;margin-left: 30px;">
            <el-input style="width: 300px;" v-model="KeyWord" placeholder="请输入内容" clearable> <el-button
                    @click="MusicSearch" slot="append" icon="el-icon-search">搜索</el-button></el-input>
            <el-button type="primary" style="margin-left: 20px;" @click="dialogVisible = true">新增</el-button>
        </div>



        <div class="manage-musiclist" style="margin-top: 30px;margin-left: 100px;">
            <el-table :data="tableData" style="width: 90%;margin-left: 30px;">
                <el-table-column prop="id" label="主键" v-if="showIdColumn"> </el-table-column>
                <el-table-column prop="music_Name" label="歌名" width="600px"> </el-table-column>
                <el-table-column prop="music_Person" label="歌手"></el-table-column>
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
            <el-dialog :visible.sync="dialogVisible" title="新增音乐" width="50%">
                <el-form>
                    <!-- 在这里添加表单字段，例如： -->
                    <el-form-item label="歌曲名称">
                        <el-input v-model="form.music_Name"></el-input>
                    </el-form-item>
                    <el-form-item label="歌手">
                        <el-input v-model="form.music_Person"></el-input>
                    </el-form-item>
                </el-form>
                <div slot="footer" class="dialog-footer">
                    <el-button @click="dialogVisible = false">取 消</el-button>
                    <el-button type="primary" @click="submitAdd">确 定</el-button>
                </div>
            </el-dialog>
        </div>


        <div>
            <!-- 编辑对话框 -->
            <el-dialog :visible.sync="dialogVisible" title="编辑音乐" width="50%">
                <el-form :model="selectedRow">
                    <el-form-item label="歌曲名称">
                        <el-input v-model="selectedRow.music_Name"></el-input>
                    </el-form-item>
                    <el-form-item label="歌手">
                        <el-input v-model="selectedRow.music_Person"></el-input>
                    </el-form-item>
                </el-form>
                <div slot="footer" class="dialog-footer">
                    <el-button @click="dialogVisible = false">取 消</el-button>
                    <el-button type="primary" @click="updateForm">确 定</el-button>
                </div>
            </el-dialog>
        </div>
    </div>
</template>

<script>
export default {
    data(){
        return{
            showIdColumn: false, // 初始隐藏id列
            dialogVisible: false, // 对话框是否可见
            KeyWord:'',
            tableData: [],
            selectedRow: {
                id: '',
                music_Name: '',
                music_Person: ''
            },
            form:{
                id:'',
                music_Name:'',
                music_Person:''
            }
        }
    },
    created(){
        try {
            this.$http.get(`/Musics/GetMusicList?KeyWord=${this.KeyWord}`).then(res => {
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
    methods:{
        MusicSearch(){
            try {
                this.$http.get(`/Musics/GetMusicList?KeyWord=${this.KeyWord}`).then(res => {
                    if (res.data.code === '200') {
                        this.tableData = res.data.data
                    } else {
                        this.$message.error(res.data.msg)
                    }
                })
            } catch(error) {
                this.$message.error('请求失败，请检查网络连接')
            }
        },
        submitAdd(){
            try{
                this.$http.post('/Musics/AddMusic',this.form).then(res => {
                    if (res.data.code === '200') {
                        this.dialogVisible=false
                        this.$message.success('新增成功')
                        this.MusicSearch()
                    } else {
                        this.$message.error(res.data.messge)
                    }
                })
            } catch(error) {
                this.$message.error('请求失败，请检查网络连接')
            }
        },
        editMusic(row) {
            this.selectedRow = row;
            this.dialogVisible = true;
        },
        updateForm(){
            try{
                this.$http.post('/Musics/EditeMusic',this.selectedRow).then(res => {
                    if (res.data.code === '200') {
                        this.dialogVisible=false
                        this.$message.success('修改成功')
                        this.MusicSearch()
                    } else {
                        this.$message.error(res.data.messge)
                    }
                })

            } catch(error) {
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
               this.$http.post('/Musics/DeleteMusic',{Id:row.id}).then(res => {
                   if (res.data.code === '200') {
                       this.$message.success('删除成功')
                       this.MusicSearch()
                   }
               })
            }).catch(() => {
                // 用户取消删除操作
                console.log('删除操作已取消');
            });
        }
    }
}
</script>
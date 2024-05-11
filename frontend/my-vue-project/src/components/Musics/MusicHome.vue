<template>
    <el-container>
        <div class="music-home" style="display: flex;">
                <div class="music-list">
                    <el-header style="margin-top: 50px;">  <el-input placeholder="请输入内容" prefix-icon="el-icon-search"></el-input></el-header>
                    <el-main></el-main>
                    <el-footer></el-footer>
                </div>

                <div class="music-content" style="display: flex;">
                    <div class="music-img">
                        <div class="music-groud"><el-image style="width: 95%; height: 100%; object-fit: cover;border-radius: 30px;" :src="require('@/image/Musics/ty.jpg')"></el-image></div>
                        <div class="music-sonner">
                            <div class="music-sonner-img"><el-image style="width: 95%; height: 100%; object-fit: cover;border-radius: 30px;" :src="require('@/image/Musics/ty1.jpeg')"></el-image></div>
                            <div class="music-sonner-img"><el-image style="width: 95%; height: 100%; object-fit: cover;border-radius: 30px;" :src="require('@/image/Musics/ty2.jpeg')"></el-image></div>
                            <div class="music-sonner-img"><el-image style="width: 95%; height: 100%; object-fit: cover;border-radius: 30px;" :src="require('@/image/Musics/ty4.jpeg')"></el-image></div>
                        </div>
                    </div>
                    <div class="music-info"><el-image style="width: 95%; height: 100%; object-fit: cover;border-radius: 30px;" :src="require('@/image/Musics/musicbf.jpg')"></el-image></div>
                </div>
        </div>
    </el-container>
</template>

<script>
</script>

<style>
 body{
    background-color: #e5e9ec;
 }
 .music-home{
    background-color: #ffffff;
    width: 80%;
    height: 1100px;
    margin-top: 3%;
    margin-left: 10%;
    border-radius: 50px;
 }

 .music-list{
    width: 20%;
    height: 100%;
    display: inline-block;
 }

.music-content{
    background-color: #f5f7f8;
    width: 79%;
    height: 97%;
    margin-top: 15px;
    border-radius: 30px;
}


.music-img{
    width: 45%;
    height:95%;
    margin-top: 35px;
    margin-left: 30px;
}

.music-info{
    width: 37%;
    height: 95%;
    margin-left: 100px;
    margin-top: 35px;
}

.music-groud{
 width: 100%;
 height: 60%;
 border-radius: 30px;
}
.music-sonner{
 margin-top: 40px;
 width: 100%;
 height: 45%;
}

.music-sonner-img{
    display: inline-block;
    width: 25%;
    height: 50%;
    margin-left: 20px;
    margin-top: 10%;
    border-radius: 30px;
}
</style>
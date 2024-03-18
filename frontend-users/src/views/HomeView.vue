<template>
  <div class="row">
    <div class="col-lg-8 offset-lg-2">
      <div class="table-responsive">
        <table class="table table-bordered table-hover">
          <thead>
            <tr>
              <th>UUID</th>
              <th>Nombre Completo</th>
              <th>Nombre de usuario</th>
              <th>Email</th>
              <th>Acciones</th>
            </tr>
          </thead>
          <tbody class="table-group-divider" id="contenido">
            <tr v-for="usr,i in users">
              <td>{{usr.login.uuid}}</td>
              <td>{{ usr.name.first}}</td>
              <td>{{ usr.login.username }}</td>
              <td>{{ usr.email }}</td> 
              <td>
                <router-link :to="{path:'edit/'+usr.login.uuid}" class="btn btn-warning">
                  🖊️
                </router-link>&nbsp;
                
                <button class="btn btn-danger">
                  X
                </button>
              </td> 
              
            </tr>
          </tbody>
        </table>
      </div>
    </div>
  </div>
</template>
<!-- se hace el codigo a modo de mostrar conocimientos ya que no se pudo realizar una conexion exitosa con la api-->
<script>
    import axios from 'axios'
    export default{
      data(){
        return{users:''}
      },
      mounted(){
        this.getUsers();
      },
      methods:{
        getUsers(){
          axios.get('https://randomuser.me/api/?results=5').then(
            response=>(
              this.users= response.data.results
            )
          )
        }
        // eliminar(id,nombre){

        // }
      }
    }
</script>
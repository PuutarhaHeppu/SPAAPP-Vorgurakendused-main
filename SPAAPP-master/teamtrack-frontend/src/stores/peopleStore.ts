import { People } from "../models/people.js";
import { ref } from "vue";
import { defineStore } from "pinia";
import useApi, { useApiRawRequest } from "../models/api.js";

export const usePeoplesStore = defineStore('PeoplesStore', () => {
  const apiGetPeoples = useApi<People[]>('Peoples');
  const Peoples = ref<People[]>([]);
  let allPeoples: People[] = [];

  const loadPeoples = async () => {
    await apiGetPeoples.request();

    if (apiGetPeoples.response.value) {
      return apiGetPeoples.response.value;
    }
    return [];
  };

  const load = async () => {
    allPeoples = await loadPeoples();
    Peoples.value = allPeoples;
  };
  const getPeopleById = (id: number) => {
    return allPeoples.find((People) => People.id === id);
  };


  const addPeople = async (People: People) => {
    const apiAddPeople = useApi<People>('Peoples', {
      method: 'POST',
      headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(People),
    }); 
    
    await apiAddPeople.request();
    if (apiAddPeople.response.value) {
      load();      
    }
  };
  const updatePeople = async (People: People) => {
    const apiUpdatePeople = useApi<People>('Peoples/' + People.id, {
      method: 'PUT',
      headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(People),
    });

    await apiUpdatePeople.request();
    if (apiUpdatePeople.response.value) {
      load();
    }    
  };


  const deletePeople = async (People: People) => {
    const deletePeopleRequest = useApiRawRequest(`Peoples/${People.id}`, {
      method: 'DELETE',
    });

    const res = await deletePeopleRequest();

    if (res.status === 204) {
      let id = Peoples.value.indexOf(People);

      if (id !== -1) {
        Peoples.value.splice(id, 1);
      }

      id = Peoples.value.indexOf(People);

      if (id !== -1) {
        Peoples.value.splice(id, 1);
      }
    }
  };

  return { Peoples, load, getPeopleById, addPeople, updatePeople, deletePeople };
});






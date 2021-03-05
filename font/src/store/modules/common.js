import { getIcons } from '../data/icon'

const state = () => ({
  iconFilterAfter: [],
  iconSources: getIcons()
})
const getters = {
  iconSources: (state) => state.iconSources,
  iconFilterAfter: (state) => state.iconFilterAfter
}
const mutations = {
  setIconFilterAfter(state, iconFilterAfter) {
    state.iconFilterAfter = iconFilterAfter
  },
  filterIcon(state, kw) {
    const filterAfter = state.iconSources.filter(x => x.iconName.indexOf(kw) != - 1)
    state.iconFilterAfter = filterAfter
  }
}
const actions = {

}

export default { state, getters, mutations, actions }